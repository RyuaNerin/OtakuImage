namespace OtakuImage
{
	partial class frmMain
	{
		/// <summary>
		/// 필수 디자이너 변수입니다.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		/// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form 디자이너에서 생성한 코드

		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다.
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.grbSearch = new System.Windows.Forms.GroupBox();
			this.chkSafeBooru = new System.Windows.Forms.CheckBox();
			this.chkSankaku = new System.Windows.Forms.CheckBox();
			this.chkRating0 = new System.Windows.Forms.CheckBox();
			this.chkEshuushuu = new System.Windows.Forms.CheckBox();
			this.chkYandere = new System.Windows.Forms.CheckBox();
			this.picAjax = new System.Windows.Forms.PictureBox();
			this.chkRatingS = new System.Windows.Forms.CheckBox();
			this.chkRatingEx = new System.Windows.Forms.CheckBox();
			this.chkRatingE = new System.Windows.Forms.CheckBox();
			this.chkRatingQ = new System.Windows.Forms.CheckBox();
			this.btnSearchPause = new System.Windows.Forms.Button();
			this.btnSearchStop = new System.Windows.Forms.Button();
			this.btnSearch = new System.Windows.Forms.Button();
			this.txtSearch = new System.Windows.Forms.TextBox();
			this.grbPreview = new System.Windows.Forms.GroupBox();
			this.cmsPreview = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmSelectAll = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmResizePreview = new System.Windows.Forms.ToolStripMenuItem();
			this.tss1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmDownload = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmDownloadAll = new System.Windows.Forms.ToolStripMenuItem();
			this.tss0 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmDel = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmClear = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmDelCompleted = new System.Windows.Forms.ToolStripMenuItem();
			this.grbDownload = new System.Windows.Forms.GroupBox();
			this.prg = new System.Windows.Forms.ProgressBar();
			this.btnDownload = new System.Windows.Forms.Button();
			this.lblDownload = new System.Windows.Forms.Label();
			this.grbPath = new System.Windows.Forms.GroupBox();
			this.btnPath = new System.Windows.Forms.Button();
			this.txtPath = new System.Windows.Forms.TextBox();
			this.fbdPath = new System.Windows.Forms.FolderBrowserDialog();
			this.bgwYandere = new System.ComponentModel.BackgroundWorker();
			this.ttp = new System.Windows.Forms.ToolTip(this.components);
			this.bgwEshuushuu = new System.ComponentModel.BackgroundWorker();
			this.bgwSearch = new System.ComponentModel.BackgroundWorker();
			this.bgwDownload = new System.ComponentModel.BackgroundWorker();
			this.bgwAjax = new System.ComponentModel.BackgroundWorker();
			this.bgwUpdata = new System.ComponentModel.BackgroundWorker();
			this.bgwSankaku = new System.ComponentModel.BackgroundWorker();
			this.bgwSafeBooru = new System.ComponentModel.BackgroundWorker();
			this.ilvPreview = new Manina.Windows.Forms.ImageListView();
			this.grbSearch.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picAjax)).BeginInit();
			this.grbPreview.SuspendLayout();
			this.cmsPreview.SuspendLayout();
			this.grbDownload.SuspendLayout();
			this.grbPath.SuspendLayout();
			this.SuspendLayout();
			// 
			// grbSearch
			// 
			this.grbSearch.Controls.Add(this.chkSafeBooru);
			this.grbSearch.Controls.Add(this.chkSankaku);
			this.grbSearch.Controls.Add(this.chkRating0);
			this.grbSearch.Controls.Add(this.chkEshuushuu);
			this.grbSearch.Controls.Add(this.chkYandere);
			this.grbSearch.Controls.Add(this.picAjax);
			this.grbSearch.Controls.Add(this.chkRatingS);
			this.grbSearch.Controls.Add(this.chkRatingEx);
			this.grbSearch.Controls.Add(this.chkRatingE);
			this.grbSearch.Controls.Add(this.chkRatingQ);
			this.grbSearch.Controls.Add(this.btnSearchPause);
			this.grbSearch.Controls.Add(this.btnSearchStop);
			this.grbSearch.Controls.Add(this.btnSearch);
			this.grbSearch.Controls.Add(this.txtSearch);
			this.grbSearch.Location = new System.Drawing.Point(12, 12);
			this.grbSearch.Name = "grbSearch";
			this.grbSearch.Size = new System.Drawing.Size(531, 95);
			this.grbSearch.TabIndex = 0;
			this.grbSearch.TabStop = false;
			this.grbSearch.Text = "검색";
			// 
			// chkSafeBooru
			// 
			this.chkSafeBooru.AutoSize = true;
			this.chkSafeBooru.Location = new System.Drawing.Point(269, 69);
			this.chkSafeBooru.Name = "chkSafeBooru";
			this.chkSafeBooru.Size = new System.Drawing.Size(83, 18);
			this.chkSafeBooru.TabIndex = 10;
			this.chkSafeBooru.Text = "SafeBooru";
			this.chkSafeBooru.UseVisualStyleBackColor = true;
			// 
			// chkSankaku
			// 
			this.chkSankaku.AutoSize = true;
			this.chkSankaku.Location = new System.Drawing.Point(190, 69);
			this.chkSankaku.Name = "chkSankaku";
			this.chkSankaku.Size = new System.Drawing.Size(73, 18);
			this.chkSankaku.TabIndex = 9;
			this.chkSankaku.Text = "Sankaku";
			this.chkSankaku.UseVisualStyleBackColor = true;
			// 
			// chkRating0
			// 
			this.chkRating0.AutoSize = true;
			this.chkRating0.Location = new System.Drawing.Point(6, 20);
			this.chkRating0.Name = "chkRating0";
			this.chkRating0.Size = new System.Drawing.Size(42, 18);
			this.chkRating0.TabIndex = 8;
			this.chkRating0.Text = "Un";
			this.ttp.SetToolTip(this.chkRating0, "Unrating");
			this.chkRating0.UseVisualStyleBackColor = true;
			// 
			// chkEshuushuu
			// 
			this.chkEshuushuu.AutoSize = true;
			this.chkEshuushuu.Location = new System.Drawing.Point(95, 69);
			this.chkEshuushuu.Name = "chkEshuushuu";
			this.chkEshuushuu.Size = new System.Drawing.Size(89, 18);
			this.chkEshuushuu.TabIndex = 7;
			this.chkEshuushuu.Text = "E-shuushuu";
			this.chkEshuushuu.UseVisualStyleBackColor = true;
			// 
			// chkYandere
			// 
			this.chkYandere.AutoSize = true;
			this.chkYandere.Checked = true;
			this.chkYandere.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkYandere.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.chkYandere.Location = new System.Drawing.Point(6, 71);
			this.chkYandere.Name = "chkYandere";
			this.chkYandere.Size = new System.Drawing.Size(83, 16);
			this.chkYandere.TabIndex = 7;
			this.chkYandere.Text = "Yande.re";
			this.chkYandere.UseVisualStyleBackColor = true;
			// 
			// picAjax
			// 
			this.picAjax.Location = new System.Drawing.Point(368, 24);
			this.picAjax.Name = "picAjax";
			this.picAjax.Size = new System.Drawing.Size(16, 16);
			this.picAjax.TabIndex = 5;
			this.picAjax.TabStop = false;
			this.picAjax.Paint += new System.Windows.Forms.PaintEventHandler(this.picAjax_Paint);
			// 
			// chkRatingS
			// 
			this.chkRatingS.AutoSize = true;
			this.chkRatingS.Checked = true;
			this.chkRatingS.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkRatingS.Location = new System.Drawing.Point(54, 20);
			this.chkRatingS.Name = "chkRatingS";
			this.chkRatingS.Size = new System.Drawing.Size(51, 18);
			this.chkRatingS.TabIndex = 4;
			this.chkRatingS.Text = "Safe";
			this.chkRatingS.UseVisualStyleBackColor = true;
			// 
			// chkRatingEx
			// 
			this.chkRatingEx.AutoSize = true;
			this.chkRatingEx.Location = new System.Drawing.Point(287, 20);
			this.chkRatingEx.Name = "chkRatingEx";
			this.chkRatingEx.Size = new System.Drawing.Size(72, 18);
			this.chkRatingEx.TabIndex = 3;
			this.chkRatingEx.Text = "Extreme";
			this.ttp.SetToolTip(this.chkRatingEx, "Extreme Content");
			this.chkRatingEx.UseVisualStyleBackColor = true;
			// 
			// chkRatingE
			// 
			this.chkRatingE.AutoSize = true;
			this.chkRatingE.Location = new System.Drawing.Point(216, 20);
			this.chkRatingE.Name = "chkRatingE";
			this.chkRatingE.Size = new System.Drawing.Size(65, 18);
			this.chkRatingE.TabIndex = 3;
			this.chkRatingE.Text = "Explicit";
			this.ttp.SetToolTip(this.chkRatingE, "성적인 내용이 짙음. 직접적인 성행위 묘사가 있거나 혹은 성기가 직접적으로 노출되는 이미지");
			this.chkRatingE.UseVisualStyleBackColor = true;
			// 
			// chkRatingQ
			// 
			this.chkRatingQ.AutoSize = true;
			this.chkRatingQ.Location = new System.Drawing.Point(111, 20);
			this.chkRatingQ.Name = "chkRatingQ";
			this.chkRatingQ.Size = new System.Drawing.Size(99, 18);
			this.chkRatingQ.TabIndex = 3;
			this.chkRatingQ.Text = "Questionable";
			this.ttp.SetToolTip(this.chkRatingQ, "성적인 내용을 강조하거나 선정적인 노출이 있음. 살짝 야릇한 상황의 그림이나 유두나 둔부의 노출이 있지만 성기의 노출이 없는 이미지");
			this.chkRatingQ.UseVisualStyleBackColor = true;
			// 
			// btnSearchPause
			// 
			this.btnSearchPause.Enabled = false;
			this.btnSearchPause.Location = new System.Drawing.Point(488, 20);
			this.btnSearchPause.Name = "btnSearchPause";
			this.btnSearchPause.Size = new System.Drawing.Size(37, 48);
			this.btnSearchPause.TabIndex = 2;
			this.btnSearchPause.Text = "일시\r\n정지";
			this.btnSearchPause.UseVisualStyleBackColor = true;
			this.btnSearchPause.Click += new System.EventHandler(this.btnSearchPause_Click);
			// 
			// btnSearchStop
			// 
			this.btnSearchStop.Enabled = false;
			this.btnSearchStop.Location = new System.Drawing.Point(445, 20);
			this.btnSearchStop.Name = "btnSearchStop";
			this.btnSearchStop.Size = new System.Drawing.Size(37, 48);
			this.btnSearchStop.TabIndex = 2;
			this.btnSearchStop.Text = "검색\r\n중지";
			this.btnSearchStop.UseVisualStyleBackColor = true;
			this.btnSearchStop.Click += new System.EventHandler(this.btnSearchStop_Click);
			// 
			// btnSearch
			// 
			this.btnSearch.Location = new System.Drawing.Point(390, 20);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(49, 48);
			this.btnSearch.TabIndex = 2;
			this.btnSearch.Text = "검색";
			this.btnSearch.UseVisualStyleBackColor = true;
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// txtSearch
			// 
			this.txtSearch.Location = new System.Drawing.Point(6, 44);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = new System.Drawing.Size(378, 21);
			this.txtSearch.TabIndex = 1;
			this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
			// 
			// grbPreview
			// 
			this.grbPreview.Controls.Add(this.ilvPreview);
			this.grbPreview.Location = new System.Drawing.Point(12, 166);
			this.grbPreview.Name = "grbPreview";
			this.grbPreview.Size = new System.Drawing.Size(531, 351);
			this.grbPreview.TabIndex = 1;
			this.grbPreview.TabStop = false;
			this.grbPreview.Text = "이미지 미리보기 : 0";
			// 
			// cmsPreview
			// 
			this.cmsPreview.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmSelectAll,
            this.tsmResizePreview,
            this.tss1,
            this.tsmDownload,
            this.tsmDownloadAll,
            this.tss0,
            this.tsmDel,
            this.tsmClear,
            this.tsmDelCompleted});
			this.cmsPreview.Name = "cmsPreview";
			this.cmsPreview.Size = new System.Drawing.Size(249, 170);
			// 
			// tsmSelectAll
			// 
			this.tsmSelectAll.Name = "tsmSelectAll";
			this.tsmSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
			this.tsmSelectAll.Size = new System.Drawing.Size(248, 22);
			this.tsmSelectAll.Text = "모두 선택";
			this.tsmSelectAll.Click += new System.EventHandler(this.tsmSelectAll_Click);
			// 
			// tsmResizePreview
			// 
			this.tsmResizePreview.Name = "tsmResizePreview";
			this.tsmResizePreview.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
			this.tsmResizePreview.Size = new System.Drawing.Size(248, 22);
			this.tsmResizePreview.Text = "미리보기 사이즈 조절";
			this.tsmResizePreview.Click += new System.EventHandler(this.tsmResizePreview_Click);
			// 
			// tss1
			// 
			this.tss1.Name = "tss1";
			this.tss1.Size = new System.Drawing.Size(245, 6);
			// 
			// tsmDownload
			// 
			this.tsmDownload.Name = "tsmDownload";
			this.tsmDownload.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
			this.tsmDownload.Size = new System.Drawing.Size(248, 22);
			this.tsmDownload.Text = "다운로드";
			this.tsmDownload.Click += new System.EventHandler(this.tsmDownload_Click);
			// 
			// tsmDownloadAll
			// 
			this.tsmDownloadAll.Name = "tsmDownloadAll";
			this.tsmDownloadAll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
						| System.Windows.Forms.Keys.D)));
			this.tsmDownloadAll.Size = new System.Drawing.Size(248, 22);
			this.tsmDownloadAll.Text = "모두 다운로드";
			this.tsmDownloadAll.Click += new System.EventHandler(this.tsmDownloadAll_Click);
			// 
			// tss0
			// 
			this.tss0.Name = "tss0";
			this.tss0.Size = new System.Drawing.Size(245, 6);
			// 
			// tsmDel
			// 
			this.tsmDel.Name = "tsmDel";
			this.tsmDel.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.tsmDel.Size = new System.Drawing.Size(248, 22);
			this.tsmDel.Text = "선택 항목 삭제";
			this.tsmDel.Click += new System.EventHandler(this.tsmDel_Click);
			// 
			// tsmClear
			// 
			this.tsmClear.Name = "tsmClear";
			this.tsmClear.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)));
			this.tsmClear.Size = new System.Drawing.Size(248, 22);
			this.tsmClear.Text = "모든 항목 삭제";
			this.tsmClear.Click += new System.EventHandler(this.tsmClear_Click);
			// 
			// tsmDelCompleted
			// 
			this.tsmDelCompleted.Name = "tsmDelCompleted";
			this.tsmDelCompleted.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)));
			this.tsmDelCompleted.Size = new System.Drawing.Size(248, 22);
			this.tsmDelCompleted.Text = "다운로드된 항목 삭제";
			this.tsmDelCompleted.Click += new System.EventHandler(this.tsmDelCompleted_Click);
			// 
			// grbDownload
			// 
			this.grbDownload.Controls.Add(this.prg);
			this.grbDownload.Controls.Add(this.btnDownload);
			this.grbDownload.Controls.Add(this.lblDownload);
			this.grbDownload.Location = new System.Drawing.Point(12, 523);
			this.grbDownload.Name = "grbDownload";
			this.grbDownload.Size = new System.Drawing.Size(531, 68);
			this.grbDownload.TabIndex = 2;
			this.grbDownload.TabStop = false;
			this.grbDownload.Text = "다운로드 대기 : 0";
			// 
			// prg
			// 
			this.prg.Location = new System.Drawing.Point(87, 39);
			this.prg.Name = "prg";
			this.prg.Size = new System.Drawing.Size(438, 23);
			this.prg.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.prg.TabIndex = 5;
			// 
			// btnDownload
			// 
			this.btnDownload.Location = new System.Drawing.Point(6, 20);
			this.btnDownload.Name = "btnDownload";
			this.btnDownload.Size = new System.Drawing.Size(75, 42);
			this.btnDownload.TabIndex = 4;
			this.btnDownload.Text = "일시정지";
			this.btnDownload.UseVisualStyleBackColor = true;
			this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
			// 
			// lblDownload
			// 
			this.lblDownload.Location = new System.Drawing.Point(87, 17);
			this.lblDownload.Name = "lblDownload";
			this.lblDownload.Size = new System.Drawing.Size(186, 19);
			this.lblDownload.TabIndex = 3;
			this.lblDownload.Text = "00.0% : 0.0 B / 0.0 B";
			this.lblDownload.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// grbPath
			// 
			this.grbPath.Controls.Add(this.btnPath);
			this.grbPath.Controls.Add(this.txtPath);
			this.grbPath.Location = new System.Drawing.Point(12, 113);
			this.grbPath.Name = "grbPath";
			this.grbPath.Size = new System.Drawing.Size(531, 47);
			this.grbPath.TabIndex = 3;
			this.grbPath.TabStop = false;
			this.grbPath.Text = "다운로드 위치";
			// 
			// btnPath
			// 
			this.btnPath.Location = new System.Drawing.Point(465, 19);
			this.btnPath.Name = "btnPath";
			this.btnPath.Size = new System.Drawing.Size(60, 21);
			this.btnPath.TabIndex = 2;
			this.btnPath.Text = "열기";
			this.btnPath.UseVisualStyleBackColor = true;
			this.btnPath.Click += new System.EventHandler(this.btnPath_Click);
			// 
			// txtPath
			// 
			this.txtPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.txtPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
			this.txtPath.BackColor = System.Drawing.SystemColors.Window;
			this.txtPath.Location = new System.Drawing.Point(6, 20);
			this.txtPath.Name = "txtPath";
			this.txtPath.ReadOnly = true;
			this.txtPath.Size = new System.Drawing.Size(453, 21);
			this.txtPath.TabIndex = 1;
			// 
			// fbdPath
			// 
			this.fbdPath.Description = "이미지를 다운받을 폴더를 설정해주세요";
			this.fbdPath.RootFolder = System.Environment.SpecialFolder.DesktopDirectory;
			// 
			// bgwYandere
			// 
			this.bgwYandere.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwYandere_DoWork);
			// 
			// ttp
			// 
			this.ttp.IsBalloon = true;
			// 
			// bgwEshuushuu
			// 
			this.bgwEshuushuu.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwEshuushuu_DoWork);
			// 
			// bgwSearch
			// 
			this.bgwSearch.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwSearch_DoWork);
			this.bgwSearch.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwSearch_RunWorkerCompleted);
			// 
			// bgwDownload
			// 
			this.bgwDownload.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwDownload_DoWork);
			// 
			// bgwAjax
			// 
			this.bgwAjax.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwAjax_DoWork);
			// 
			// bgwUpdata
			// 
			this.bgwUpdata.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwUpdata_DoWork);
			// 
			// bgwSankaku
			// 
			this.bgwSankaku.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwSankaku_DoWork);
			// 
			// bgwSafeBooru
			// 
			this.bgwSafeBooru.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwSafeBooru_DoWork);
			// 
			// ilvPreview
			// 
			this.ilvPreview.AllowDrag = true;
			this.ilvPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ilvPreview.CacheLimit = "30MB";
			this.ilvPreview.ContextMenuStrip = this.cmsPreview;
			this.ilvPreview.DefaultImage = global::OtakuImage.Properties.Resources.refresh;
			this.ilvPreview.ErrorImage = global::OtakuImage.Properties.Resources.err;
			this.ilvPreview.HeaderFont = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.ilvPreview.Location = new System.Drawing.Point(6, 20);
			this.ilvPreview.Name = "ilvPreview";
			this.ilvPreview.Size = new System.Drawing.Size(519, 325);
			this.ilvPreview.TabIndex = 4;
			this.ilvPreview.Text = "";
			this.ilvPreview.UseEmbeddedThumbnails = Manina.Windows.Forms.UseEmbeddedThumbnails.Always;
			this.ilvPreview.ItemDoubleClick += new Manina.Windows.Forms.ItemDoubleClickEventHandler(this.ilvPreview_ItemDoubleClick);
			// 
			// frmMain
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(555, 603);
			this.Controls.Add(this.grbPreview);
			this.Controls.Add(this.grbSearch);
			this.Controls.Add(this.grbDownload);
			this.Controls.Add(this.grbPath);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("나눔고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(571, 641);
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.Shown += new System.EventHandler(this.frmMain_Shown);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
			this.Resize += new System.EventHandler(this.frmMain_Resize);
			this.grbSearch.ResumeLayout(false);
			this.grbSearch.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picAjax)).EndInit();
			this.grbPreview.ResumeLayout(false);
			this.cmsPreview.ResumeLayout(false);
			this.grbDownload.ResumeLayout(false);
			this.grbPath.ResumeLayout(false);
			this.grbPath.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox grbSearch;
		private System.Windows.Forms.TextBox txtSearch;
		private System.Windows.Forms.GroupBox grbPreview;
		private System.Windows.Forms.GroupBox grbDownload;
		private System.Windows.Forms.Button btnSearchStop;
		private System.Windows.Forms.Button btnSearch;
		private System.Windows.Forms.GroupBox grbPath;
		private System.Windows.Forms.TextBox txtPath;
		private System.Windows.Forms.FolderBrowserDialog fbdPath;
		private System.Windows.Forms.Label lblDownload;
		private System.ComponentModel.BackgroundWorker bgwYandere;
		private System.Windows.Forms.CheckBox chkRatingEx;
		private System.Windows.Forms.CheckBox chkRatingQ;
		private System.Windows.Forms.CheckBox chkRatingE;
		private System.Windows.Forms.ToolTip ttp;
		private System.Windows.Forms.Button btnSearchPause;
		private System.Windows.Forms.CheckBox chkRatingS;
		private System.ComponentModel.BackgroundWorker bgwEshuushuu;
		private System.ComponentModel.BackgroundWorker bgwSearch;
		private System.Windows.Forms.Button btnDownload;
		private System.ComponentModel.BackgroundWorker bgwDownload;
		private System.Windows.Forms.Button btnPath;
		private System.Windows.Forms.PictureBox picAjax;
		private System.ComponentModel.BackgroundWorker bgwAjax;
		private Manina.Windows.Forms.ImageListView ilvPreview;
		private System.ComponentModel.BackgroundWorker bgwUpdata;
		private System.Windows.Forms.ContextMenuStrip cmsPreview;
		private System.Windows.Forms.ToolStripMenuItem tsmDel;
		private System.Windows.Forms.ToolStripMenuItem tsmClear;
		private System.Windows.Forms.ToolStripMenuItem tsmDownload;
		private System.Windows.Forms.ToolStripSeparator tss0;
		private System.Windows.Forms.ToolStripMenuItem tsmDelCompleted;
		private System.Windows.Forms.ToolStripMenuItem tsmDownloadAll;
		private System.Windows.Forms.ToolStripMenuItem tsmSelectAll;
		private System.Windows.Forms.ToolStripMenuItem tsmResizePreview;
		private System.Windows.Forms.ToolStripSeparator tss1;
		private System.Windows.Forms.CheckBox chkEshuushuu;
		private System.Windows.Forms.CheckBox chkYandere;
		private System.Windows.Forms.ProgressBar prg;
		private System.Windows.Forms.CheckBox chkRating0;
		private System.Windows.Forms.CheckBox chkSankaku;
		private System.Windows.Forms.CheckBox chkSafeBooru;
		private System.ComponentModel.BackgroundWorker bgwSankaku;
		private System.ComponentModel.BackgroundWorker bgwSafeBooru;
	}
}

