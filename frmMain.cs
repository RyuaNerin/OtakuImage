using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;

using Manina.Windows.Forms;

using ImageListView = Manina.Windows.Forms.ImageListView;
using ComputerBeacon.Json;

namespace OtakuImage
{	
	public partial class frmMain : Form
	{
		private struct stDownload
		{
			public stDownload(string url, string path)
			{
				this.PostURL = url;
				this.PostPath = path;
			}
			public string PostURL;
			public string PostPath;
		}
		private struct stPreview
		{
			public enum DownloadState { None = 0, Waiting = 1, Downloading = 2, Complete = 3, Error = 4 }
			public stPreview(string url)
			{
				this.URL = url;
				this.State = DownloadState.None;
			}
			public string URL;
			public DownloadState State;
		}

		public class PreviewRenderer : Manina.Windows.Forms.ImageListView.ImageListViewRenderer
		{
			public override Size MeasureItem(Manina.Windows.Forms.View view)
			{
				Size itemSize = new Size();

				Size itemPadding = new Size(2, 2);
				itemSize = ImageListView.ThumbnailSize + itemPadding + itemPadding;
				return itemSize;
			}

			public override void DrawItem(System.Drawing.Graphics g, ImageListViewItem item, ItemState state, System.Drawing.Rectangle bounds)
			{
				using (Brush bItemBack = new SolidBrush(item.BackColor))
					g.FillRectangle(bItemBack, bounds);

				Size itemPadding = new Size(2, 2);

				Image img = item.ThumbnailImage;
				if (img != null)
				{
					Rectangle border = new Rectangle(bounds.Location + itemPadding, ImageListView.ThumbnailSize);
					Rectangle pos = GetSizedImageBounds(img, border);
					g.DrawImage(img, pos);

					// Draw image border
					if (ImageListView.Focused && ((state & ItemState.Selected) != ItemState.None))
					{
						using (Pen pen = new Pen(SystemColors.Highlight, 3))
						{
							g.DrawRectangle(pen, border);
						}
					}
					else if (!ImageListView.Focused && ((state & ItemState.Selected) != ItemState.None))
					{
						using (Pen pen = new Pen(SystemColors.GrayText, 3))
						{
							pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;
							g.DrawRectangle(pen, border);
						}
					}
					else
					{
						using (Pen pGray128 = new Pen(Color.FromArgb(128, SystemColors.GrayText)))
						{
							g.DrawRectangle(pGray128, border);
						}
					}

					stPreview st = (stPreview)item.Tag;
					
					if (st.State == stPreview.DownloadState.Downloading)
						g.DrawImageUnscaled(OtakuImage.Properties.Resources.downloading, border.Left + 5, border.Top + 5);

					else if (st.State == stPreview.DownloadState.Waiting)
						g.DrawImageUnscaled(OtakuImage.Properties.Resources.waiting, border.Left + 5, border.Top + 5);

					else if (st.State == stPreview.DownloadState.Complete)
						g.DrawImageUnscaled(OtakuImage.Properties.Resources.complete, border.Left + 5, border.Top + 5);

					else if (st.State == stPreview.DownloadState.Error)
						g.DrawImageUnscaled(OtakuImage.Properties.Resources.err, border.Left + 5, border.Top + 5);
				}
			}
			private Rectangle GetSizedImageBounds(Image image, Rectangle fit, float hAlign, float vAlign)
			{
				Size scaled = GetSizedImageBounds(image, fit.Size);
				int x = fit.Left + (int)(hAlign / 100.0f * (float)(fit.Width - scaled.Width));
				int y = fit.Top + (int)(vAlign / 100.0f * (float)(fit.Height - scaled.Height));

				return new Rectangle(x, y, scaled.Width, scaled.Height);
			}
			private Rectangle GetSizedImageBounds(Image image, Rectangle fit)
			{
				return GetSizedImageBounds(image, fit, 50.0f, 50.0f);
			}
			private Size GetSizedImageBounds(Image image, Size fit)
			{
				float f = System.Math.Max((float)image.Width / (float)fit.Width, (float)image.Height / (float)fit.Height);
				if ((f < 1.0f) && (image.Width == 16)) f = 1.0f;
				int width = (int)System.Math.Round((float)image.Width / f);
				int height = (int)System.Math.Round((float)image.Height / f);
				return new Size(width, height);
			}
		}

		bool bExit = false;
		bool bPause = false;

		bool bRatingS, bRatingQ, bRatingE, bRatingEx, bRating0;

		WebClient oDownload = new WebClient();

		public frmMain()
		{
			InitializeComponent();
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			this.ilvPreview.SetRenderer(new PreviewRenderer());

			this.oDownload.DownloadProgressChanged += new DownloadProgressChangedEventHandler(oDownload_DownloadProgressChanged);
			this.oDownload.DownloadFileCompleted += new AsyncCompletedEventHandler(oDownload_DownloadFileCompleted);

			this.Text = String.Format("오타킈 짤수집기 v{0} / by @_RyuaRin", Application.ProductVersion);
		}

		private string ConvertCapacity(long i)
		{
			try
			{
				if (i < 1000)
					return String.Format("{0:##0.0} B", i);
				else if (i < 1024 * 1000)
					return String.Format("{0:##0.0} KB", (i / 1024d));
				else if (i < 1024 * 1024 * 1000)
					return String.Format("{0:##0.0} MB", (i / 1024d / 1024d));
				else
					return String.Format("{0:##0.0} GB", (i / 1024d / 1024d / 1024d));
			}
			catch
			{
				return string.Format("{0} B", i);
			}
		}

		private void frmMain_Resize(object sender, EventArgs e)
		{
			this.grbSearch.Width = this.Width - 40;
			this.txtSearch.Width = this.Width - 193;
			this.btnSearch.Left = this.Width - 181;
			this.btnSearchPause.Left = this.Width - 126;
			this.btnSearchStop.Left = this.Width - 83;

			this.grbPath.Width = this.Width - 40;
			this.txtPath.Width = this.Width - 118;
			this.btnPath.Left = this.Width - 106;

			this.grbPreview.Width = this.Width - 40;
			this.grbPreview.Height = this.Height - 290;
			this.ilvPreview.Width = this.Width - 52;
			this.ilvPreview.Height = this.Height - 316;

			this.grbDownload.Width = this.Width - 40;
			this.grbDownload.Top = this.Height - 118;
			this.prg.Width = this.Width - 133;
		}

		private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.bExit = true;
			this.Enabled = false;
			while (this.bgwSearch.IsBusy)
				Application.DoEvents();

			this.bDownloadExit = true;
			while (this.bgwDownload.IsBusy)
				Application.DoEvents();

			this.ilvPreview.Items.Clear();

			// 캐시 비우기
			string sPath = (string)this.Invoke(new delegate_s(delegate() { return this.txtPath.Text; }));
			sPath = Path.Combine(sPath, "cache");

			try
			{
				if (Directory.Exists(sPath))
					Directory.Delete(sPath, true);
			}
			catch { }
		}

		private void frmMain_Shown(object sender, EventArgs e)
		{
			if (this.fbdPath.ShowDialog() != DialogResult.OK)
			{
				this.Close();
				return;
			}

			this.txtPath.Text = this.fbdPath.SelectedPath;

			this.Focus();

#if !DEBUG
			this.bgwUpdata.RunWorkerAsync();
#endif
			this.bgwDownload.RunWorkerAsync();
		}

		private void bgwUpdata_DoWork(object sender, DoWorkEventArgs e)
		{
// 			WebClient ow = new WebClient();
// 			string sHTTP = ow.DownloadString("http://ryuanerin.kr/server/otaku_booru/ver");
// 			if (sHTTP != Application.ProductVersion)
// 			{
// 				this.bgwUpdata_DoWork_New(String.Format("새로운 버전이 있습니다!\n{0} -> {1}", Application.ProductVersion, sHTTP));
// 			}
		}
		private delegate void bgwUpdata_DoWork_New_d(string s);
		private void bgwUpdata_DoWork_New(string s)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new bgwUpdata_DoWork_New_d(bgwUpdata_DoWork_New), s);
			}
			else
			{
				MessageBox.Show(s, "오타킈 짤수집기");
				System.Diagnostics.Process.Start("explorer", "http://ryuanerin.kr/server/otaku_booru/otaku.exe");

				this.Close();
			}
		}

		private void btnPath_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("explorer", this.txtPath.Text);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Enter)
				this.btnSearch_Click();
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			this.btnSearch_Click();
		}
		private void btnSearch_Click()
		{
			if (!this.bPause)
			{
				this.btnSearch.Enabled = false;
				this.btnSearchStop.Enabled = true;
				this.btnSearchPause.Enabled = true;

				this.bPause = false;
				this.bExit = false;

				this.bRating0 = this.chkRating0.Checked;
				this.bRatingS = this.chkRatingS.Checked;
				this.bRatingQ = this.chkRatingQ.Checked;
				this.bRatingE = this.chkRatingE.Checked;
				this.bRatingEx = this.chkRatingEx.Checked;

				this.txtSearch.Enabled = false;
				this.chkRating0.Enabled = this.chkRatingS.Enabled = this.chkRatingQ.Enabled = this.chkRatingE.Enabled = this.chkRatingEx.Enabled = false;

				this.bgwSearch.RunWorkerAsync(this.txtSearch.Text.Trim());
			}
			else
			{
				this.btnSearch.Enabled = false;
				this.btnSearchPause.Enabled = true;
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private void btnSearchStop_Click(object sender, EventArgs e)
		{
			this.bPause = false;
			this.bExit = true;

			this.btnSearch.Enabled = false;
			this.btnSearchStop.Enabled = false;
			this.btnSearchPause.Enabled = false;
		}
		private void btnSearchPause_Click(object sender, EventArgs e)
		{
			this.bPause = false;

			this.btnSearch.Enabled = true;
			this.btnSearchPause.Enabled = false;
		}

		private string MakePath(string url)
		{
			string sPath = (string)this.Invoke(new delegate_s(delegate() { return this.txtPath.Text; }));
			sPath = Path.Combine(sPath, "cache");
			if (!Directory.Exists(sPath))
				Directory.CreateDirectory(sPath);
			sPath = Path.Combine(sPath, DateTime.Now.ToString("yyyyMMddHHmmssfffffff"));
			sPath = sPath + url.Substring(url.LastIndexOf("."));
			if (sPath.IndexOf('?') >= 0)
				sPath = sPath.Substring(0, sPath.LastIndexOf('?'));

			return sPath;
		}

		private AutoResetEvent AddPreview_ARE = new AutoResetEvent(true);
		private void AddPreview(WebClient ow, string postURL, string postThumb)
		{
			postURL = postURL.Replace("&amp;", "&");
			postThumb = postThumb.Replace("&amp;", "&");

			while (bPause)
				Thread.Sleep(500);
			if (bExit) return;

			string path = MakePath(postThumb);

			try
			{
				ow.Headers.Set(HttpRequestHeader.Referer, postThumb);
				ow.Headers.Set(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.117 Safari/537.36");
				ow.DownloadFile(postThumb, path);

				if (ow.ResponseHeaders[HttpResponseHeader.ContentType].StartsWith("image/"))
				{
					AddPreview2(postURL, path);
				}
				else
				{
					if (File.Exists(path))
						File.Delete(path);
				}
			}
			catch
			{
				
			}

		}
		private delegate void AddPreview2_d(string postURL, string path);
		private void AddPreview2(string postURL, string path)
		{
			if (this.InvokeRequired)
			{
				this.AddPreview_ARE.WaitOne();
				this.Invoke(new AddPreview2_d(AddPreview2), postURL, path);
				this.AddPreview_ARE.Set();
			}
			else
			{
				while (bPause)
					Thread.Sleep(500);
				if (bExit) return;

				ImageListViewItem item = new ImageListViewItem();
				item.FileName = path;


				stPreview st = new stPreview(postURL);

				int r = (new Random(DateTime.Now.Millisecond)).Next(0, 4);

				st.State = stPreview.DownloadState.None;

				item.Tag = st;

				this.ilvPreview.Items.Add(item);

				this.grbPreview.Text = "이미지 미리보기 : " + this.ilvPreview.Items.Count;
			}
		}

		List<stDownload> downloadList = new List<stDownload>();
		object downloadSync = new object();

		bool bDownload = true;
		AutoResetEvent Download_Async = new AutoResetEvent(false);
		bool bIsError = false;
		bool bDownloadExit = false;

		private delegate stDownload delegate_st();
		private void bgwDownload_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				while (!bDownload && !this.bDownloadExit)
					Thread.Sleep(500);

				int iCount;
				stDownload st;

				string sPath = (string)this.Invoke(new delegate_s(delegate() { return this.txtPath.Text; }));
				string sPathFile;

				while (!this.bDownloadExit)
				{
					while (!bDownload && !this.bDownloadExit)
						Thread.Sleep(500);

					lock (downloadSync)
						iCount = (int)this.Invoke(new delegate_i(delegate() { return this.downloadList.Count; }));

					if (iCount > 0)
					{
						lock (downloadSync)
							st = (stDownload)this.Invoke(new delegate_st(delegate() { return this.downloadList[0]; }));

						sPathFile = st.PostPath.Substring(st.PostPath.LastIndexOf(@"\") + 1);
						sPathFile = Path.Combine(sPath, sPathFile);

						this.bgwDownload_DoWork_SetState(st.PostURL, stPreview.DownloadState.Downloading);

						this.Download_Async.Reset();
						this.oDownload.Headers.Set(HttpRequestHeader.Referer, st.PostURL);
						this.oDownload.Headers.Set(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.117 Safari/537.36");
						this.oDownload.DownloadFileAsync(new Uri(st.PostURL), sPathFile);
						this.Download_Async.WaitOne();

						if (!this.bIsError)
							this.bgwDownload_DoWork_SetState(st.PostURL, stPreview.DownloadState.Complete);
						else
							this.bgwDownload_DoWork_SetState(st.PostURL, stPreview.DownloadState.Error);

						this.Invoke(new delegate_v(
							delegate()
							{
								lock (downloadSync)
								{
									this.downloadList.RemoveAt(0);

									this.grbDownload.Text = "다운로드 대기 : " + this.downloadList.Count;
								}
							}
							));
					}
					else
					{
						Thread.Sleep(500);
					}
				}
			}
			catch
			{
			}
		}
		
		private delegate void bgwDownload_DoWork_SetState_d(string url, stPreview.DownloadState NewState);
		private void bgwDownload_DoWork_SetState(string url, stPreview.DownloadState NewState)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new bgwDownload_DoWork_SetState_d(bgwDownload_DoWork_SetState), url, NewState);
			}
			else
			{
				try
				{
					stPreview st;
					for (int i = 0; i < this.ilvPreview.Items.Count; i++)
					{
						st = (stPreview)this.ilvPreview.Items[i].Tag;
						if (st.URL == url)
						{
							st.State = NewState;
							this.ilvPreview.Items[i].Tag = st;
							this.ilvPreview.Items[i].Update();
							break;
						}
					}
				}
				catch
				{

				}
			}
		}

		private void oDownload_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
		{
			if (e.Error != null || e.Cancelled == true)
				this.bIsError = true;
			else
				this.bIsError = false;

			this.Download_Async.Set();
		}
		private void oDownload_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			this.Invoke
			(
				new delegate_v
				(
					delegate()
					{

						this.lblDownload.Text = String.Format(
							"{0:#00}% : {1} / {2}",
							e.ProgressPercentage,
							this.ConvertCapacity(e.BytesReceived),
							this.ConvertCapacity(e.TotalBytesToReceive));

						this.prg.Value = e.ProgressPercentage;
					}
				)
			);
		}

		private void btnDownload_Click(object sender, EventArgs e)
		{
			if (!this.bDownload)
			{
				this.bDownload = true;
				this.btnDownload.Text = "일시정지";
			}
			else
			{
				this.bDownload = false;
				this.btnDownload.Text = "다운로드";
			}
		}

		private void ilvPreview_ItemDoubleClick(object sender, ItemClickEventArgs e)
		{
			stPreview st = (stPreview)e.Item.Tag;

			if ((st.State != stPreview.DownloadState.None) && (st.State != stPreview.DownloadState.Error))
				return;

			st.State = stPreview.DownloadState.Waiting;
			e.Item.Tag = st;

			stDownload st2 = new stDownload(st.URL, e.Item.FileName);

			lock (downloadSync)
			{
				this.downloadList.Add(st2);

				this.grbDownload.Text = "다운로드 대기 : " + this.downloadList.Count;
			}
		}

		int byAjax = 24;
		private void picAjax_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(this.picAjax.BackColor);

			if (byAjax < 24)
				e.Graphics.DrawImage(Properties.Resources.Ajax16, new Rectangle(0, 0, 16, 16), new Rectangle(0, byAjax * 16, 16, 16), GraphicsUnit.Pixel);
		}
		private void bgwAjax_DoWork(object sender, DoWorkEventArgs e)
		{
			while (this.bgwSearch.IsBusy)
			{
				this.byAjax = ++this.byAjax % 24;
				this.picAjax.Invalidate();
				Thread.Sleep(50);
			}

			this.byAjax = 24;
			this.picAjax.Invalidate();
		}

		private delegate void delegate_v();
		private delegate string delegate_s();
		private delegate int delegate_i();
		private delegate bool delegate_b();

#region SearchingEngine
		private void bgwSearch_DoWork(object sender, DoWorkEventArgs e)
		{
			string tags = (string)e.Argument;

			this.bgwAjax.RunWorkerAsync();

			if ((bool)this.Invoke(new delegate_b(delegate() { return this.chkEshuushuu.Checked; })))
				this.bgwEshuushuu.RunWorkerAsync(tags);

			if ((bool)this.Invoke(new delegate_b(delegate() { return this.chkYandere.Checked; })))
				this.bgwYandere.RunWorkerAsync(tags);

			if ((bool)this.Invoke(new delegate_b(delegate() { return this.chkSankaku.Checked; })))
				this.bgwSankaku.RunWorkerAsync(tags);

			if ((bool)this.Invoke(new delegate_b(delegate() { return this.chkSafeBooru.Checked; })))
				this.bgwSafeBooru.RunWorkerAsync(tags);

			while
				(
				this.bgwEshuushuu.IsBusy ||
				this.bgwSankaku.IsBusy ||
				this.bgwSafeBooru.IsBusy ||
				this.bgwYandere.IsBusy
				)
				Thread.Sleep(500);
		}
		private void bgwSearch_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.btnSearch.Enabled = true;
			this.btnSearchStop.Enabled = false;
			this.btnSearchPause.Enabled = false;

			this.bPause = false;
			this.bExit = false;

			this.txtSearch.Text = "";
			this.txtSearch.Enabled = true;
			this.txtSearch.Focus();
			this.chkRating0.Enabled = this.chkRatingS.Enabled = this.chkRatingQ.Enabled = this.chkRatingE.Enabled = this.chkRatingEx.Enabled = true;
		}



		Regex regexYandere = new Regex("\"thumb\" href=\"([^\"]+)\" ><img src=\"([^\"]+)\" [^>]+ alt=\"([^\"]+)\"", RegexOptions.Compiled);
		Regex regexYandereImage = new Regex("<a class=\"original-file-unchanged\" href=\"([^\">]+)\" [^>]+>|<a class=\"original-file-changed\" href=\"([^\">]+)\" [^>]+>", RegexOptions.Compiled);
		private void bgwYandere_DoWork(object sender, DoWorkEventArgs e)
		{
			string tags = (string)e.Argument;
			tags = Uri.EscapeDataString(tags);

			string sHTML, postURL, postThumb, postRating;
			bool rating;
			MatchCollection mc;
			int iPage = 0;

			using (WebClient ow = new WebClient())
			{
				while (true)
				{
					while (bPause)
						Thread.Sleep(500);
					if (bExit) break;

					iPage++;

					sHTML = ow.DownloadString(String.Format("http://yande.re/post?page={0}&tags={1}", iPage, tags));

					mc = this.regexYandere.Matches(sHTML);

					if (mc.Count == 0)
						return;

					for (int i = 0; i < mc.Count; i++)
					{
						try
						{
							while (bPause)
								Thread.Sleep(500);
							if (bExit) break;

							postRating = mc[i].Result("$3");

							rating = false;
							if (this.bRatingS && (postRating.IndexOf("Rating: Safe") >= 0))
								rating = true;

							if (this.bRatingQ && (postRating.IndexOf("Rating: Questionable") >= 0))
								rating = true;

							if (this.bRatingE && (postRating.IndexOf("Rating: Explicit") >= 0))
								rating = true;

							if (this.bRatingEx && (postRating.IndexOf("extreme_content") >= 0))
								rating = true;

							if (!rating)
								continue;

							postURL = mc[i].Result("http://yande.re$1");
							postThumb = mc[i].Result("$2");

							// postURL 얻기

							sHTML = ow.DownloadString(postURL);
							postURL = this.regexYandereImage.Match(sHTML).Result("$1$2");

							AddPreview(ow, postURL, postThumb);
						}
						catch
						{
						}
					}

					Thread.Sleep(1000);
				}
			}
		}



		Regex regexEshuushuu = new Regex("<a class=\"thumb_image\" href=\"([^\"]+)\" target=\"_blank\">[^/]+<img src=\"([^\"]+)\"", RegexOptions.Compiled);
		Regex regexEshuushuuPage = new Regex("<a href=\"?page=[0-9]+;tags=[0123456789+]+\">", RegexOptions.Compiled);
		private void bgwEshuushuu_DoWork(object sender, DoWorkEventArgs e)
		{

			string tags = (string)e.Argument;
			tags = "\"" + tags.Replace(" ", "\" \"") + "\"";
			tags = Uri.EscapeDataString(tags);

			string tagsKey;

			string sHTML, postURL, postThumb;
			MatchCollection mc;

			using (WebClient ow = new WebClient())
			{
				sHTML = ow.UploadString("http://e-shuushuu.net/search/process/", "source=&char=&artist=&postcontent=&txtposter=&thumbs=1&tags=" + tags);
				tagsKey = this.regexEshuushuuPage.Match(sHTML).Result("$1");

				int iPage = 0;
				while (true)
				{
					while (bPause)
						Thread.Sleep(500);
					if (bExit) break;

					iPage++;

					sHTML = ow.DownloadString(String.Format("http://e-shuushuu.net/search/results/?page={0}&tags={1}", iPage, tagsKey));

					mc = this.regexEshuushuu.Matches(sHTML);

					if (mc.Count == 0)
						return;

					for (int i = 0; i < mc.Count; i++)
					{
						while (bPause)
							Thread.Sleep(500);
						if (bExit) break;

						postURL = mc[i].Result("http://e-shuushuu.net/$1");
						postThumb = mc[i].Result("$2");
						AddPreview(ow, postURL, postThumb);
					}

					Thread.Sleep(1000);
				}
			}
		}
		


		Regex regexSankaku = new Regex("<a href=\"(/post/show/[0-9]+)\" onclick=\"[^\"]+\"><img class=preview src=\"([^\"]+)\" title=\"([^\"]+)\"", RegexOptions.Compiled);
		Regex regexSankakuImage = new Regex("<a href=\"([^\"]+)\" id=highres", RegexOptions.Compiled);
		private void bgwSankaku_DoWork(object sender, DoWorkEventArgs e)
		{
			string tags = (string)e.Argument;
			tags = Uri.EscapeDataString(tags);

			string sHTML, postURL, postThumb, postRating;
			bool rating;
			MatchCollection mc;
			int iPage = 0;

			using (WebClient ow = new WebClient())
			{
				while (true)
				{
					while (bPause)
						Thread.Sleep(500);
					if (bExit) break;

					iPage++;
					try
					{
						ow.Headers.Set(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.117 Safari/537.36");
						sHTML = ow.DownloadString(
							"http://chan.sankakucomplex.com/post/index.content?page=" + iPage + (tags.Length > 0 ? "&tags=" + tags : ""));
					}
					catch
					{
						sHTML = "";
					}

					mc = this.regexSankaku.Matches(sHTML);

					if (mc.Count == 0)
						return;

					for (int i = 0; i < mc.Count; i++)
					{
						try
						{
							while (bPause)
								Thread.Sleep(500);
							if (bExit) break;

							postRating = mc[i].Groups[3].Value;

							rating = false;
							if (this.bRatingS && (postRating.IndexOf("Rating:Safe") >= 0))
								rating = true;

							if (this.bRatingQ && (postRating.IndexOf("Rating:Questionable") >= 0))
								rating = true;

							if (this.bRatingE && (postRating.IndexOf("Rating:Explicit") >= 0))
								rating = true;

							if (this.bRatingEx && (postRating.IndexOf("extreme_content") >= 0))
								rating = true;

							if (!rating)
								continue;

							postURL = "http://chan.sankakucomplex.com" + mc[i].Groups[1].Value;
							postThumb = mc[i].Groups[2].Value;

							// postURL 얻기
							ow.Headers.Set(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.117 Safari/537.36");
							sHTML = ow.DownloadString(postURL);
							postURL = this.regexSankakuImage.Match(sHTML).Result("$1");

							AddPreview(ow, postURL, postThumb);

							Thread.Sleep(500);
						}
						catch
						{
						}
					}

					Thread.Sleep(1000);
				}
			}
		}




		Regex regexSafeBooru = new Regex("<a id=\"[^\"]+\" href=\"([^\"]+)\" ><img src=\"([^\"]+)\" alt=\"[^\"]+\" border=\"[^\"]+\" title=\"([^\"]+)\"", RegexOptions.Compiled);
		Regex regexSafeBooruImage = new Regex("<a href=\"[^\"]+\" style=\"font-weight: bold;\">Original image</a>", RegexOptions.Compiled);
		private void bgwSafeBooru_DoWork(object sender, DoWorkEventArgs e)
		{
			string tags = (string)e.Argument;
			tags = Uri.EscapeDataString(tags);

			string sHTML, postURL, postThumb, postRating;
			bool rating;
			MatchCollection mc;
			int iPage = 0;

			using (WebClient ow = new WebClient())
			{
				while (true)
				{
					while (bPause)
						Thread.Sleep(500);
					if (bExit) break;

					iPage++;

					sHTML = ow.DownloadString(String.Format("http://safebooru.org/index.php?page=post&s=list&pid={0}&tags={1}", iPage * 40 - 40, tags));

					mc = this.regexSafeBooru.Matches(sHTML);

					if (mc.Count == 0)
						return;

					for (int i = 0; i < mc.Count; i++)
					{
						try
						{
							while (bPause)
								Thread.Sleep(500);
							if (bExit) break;

							postRating = mc[i].Groups[3].Value;

							rating = false;
							if (this.bRatingS && (postRating.IndexOf("rating:Safe") >= 0))
								rating = true;

							if (this.bRatingQ && (postRating.IndexOf("rating:Questionable") >= 0))
								rating = true;

							if (this.bRatingE && (postRating.IndexOf("rating:Explicit") >= 0))
								rating = true;

							if (this.bRatingEx && (postRating.IndexOf("extreme_content") >= 0))
								rating = true;

							if (!rating)
								continue;

							postURL = "http://safebooru.com/" + mc[i].Groups[1].Value.Replace("&amp;", "&");
							postThumb = mc[i].Groups[2].Value;

							// postURL 얻기
							sHTML = ow.DownloadString(postURL);
							postURL = this.regexSafeBooruImage.Match(sHTML).Result("$1$2");

							AddPreview(ow, postURL, postThumb);
						}
						catch
						{
						}
					}

					Thread.Sleep(1000);
				}
			}
		}

#endregion

		private void tsmDel_Click(object sender, EventArgs e)
		{
			while (this.ilvPreview.SelectedItems.Count > 0)
			{
				try
				{
					this.ilvPreview.Items.RemoveAt(this.ilvPreview.SelectedItems[0].Index);
				}
				catch
				{
				}
			}

			this.grbPreview.Text = "이미지 미리보기 : " + this.ilvPreview.Items.Count;
		}
		private void tsmClear_Click(object sender, EventArgs e)
		{
			this.ilvPreview.Items.Clear();

			this.grbPreview.Text = "이미지 미리보기 : " + this.ilvPreview.Items.Count;
		}
		private void tsmDownload_Click(object sender, EventArgs e)
		{
			stPreview st;
			stDownload st2;

			for (int i = 0; i < this.ilvPreview.Items.Count; i++)
			{
				if (!this.ilvPreview.Items[i].Selected)
					continue;

				try
				{
					st = (stPreview)this.ilvPreview.Items[i].Tag;

					if ((st.State != stPreview.DownloadState.None) && (st.State != stPreview.DownloadState.Error))
						continue;

					st.State = stPreview.DownloadState.Waiting;

					this.ilvPreview.Items[i].Tag = st;
					this.ilvPreview.Items[i].Update();

					st2 = new stDownload(st.URL, this.ilvPreview.Items[i].FileName);

					lock (downloadSync)
						this.downloadList.Add(st2);
				}
				catch
				{
				}
			}

			lock (downloadSync)
				this.grbDownload.Text = "다운로드 대기 : " + this.downloadList.Count;
		}
		private void tsmDelCompleted_Click(object sender, EventArgs e)
		{
			stPreview st;
			int i = 0;

			while (i < this.ilvPreview.Items.Count)
			{
				try
				{
					st = (stPreview)this.ilvPreview.Items[i].Tag;

					if (st.State == stPreview.DownloadState.Complete)
						this.ilvPreview.Items.RemoveAt(i);
					else
						i++;
				}
				catch
				{
					i++;
				}
			}

			this.grbPreview.Text = "이미지 미리보기 : " + this.ilvPreview.Items.Count;
		}
		private void tsmDownloadAll_Click(object sender, EventArgs e)
		{
			stPreview st;
			stDownload st2;

			for (int i = 0; i < this.ilvPreview.Items.Count; i++)
			{
				try
				{
					st = (stPreview)this.ilvPreview.Items[i].Tag;

					if ((st.State != stPreview.DownloadState.None) && (st.State != stPreview.DownloadState.Error))
						continue;

					st.State = stPreview.DownloadState.Waiting;

					this.ilvPreview.Items[i].Tag = st;
					this.ilvPreview.Items[i].Update();

					st2 = new stDownload(st.URL, this.ilvPreview.Items[i].FileName);

					lock (downloadSync)
						this.downloadList.Add(st2);
				}
				catch
				{
				}
			}

			lock (downloadSync)
				this.grbDownload.Text = "다운로드 대기 : " + this.downloadList.Count;
		}
		private void tsmSelectAll_Click(object sender, EventArgs e)
		{
			this.ilvPreview.SelectAll();
		}
		private void tsmResizePreview_Click(object sender, EventArgs e)
		{
			if (this.ilvPreview.ThumbnailSize.Width == 256)
				this.ilvPreview.ThumbnailSize = new Size(96, 96);

			else if (this.ilvPreview.ThumbnailSize.Width == 96)
				this.ilvPreview.ThumbnailSize = new Size(128, 128);

			else if (this.ilvPreview.ThumbnailSize.Width == 128)
				this.ilvPreview.ThumbnailSize = new Size(192, 192);

			else if (this.ilvPreview.ThumbnailSize.Width == 192)
				this.ilvPreview.ThumbnailSize = new Size(256, 256);
		}
	}
}