namespace RssReader {
    partial class Form1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            cbUrl = new ComboBox();
            btRssGet = new Button();
            lbTitles = new ListBox();
            wvRssLink = new Microsoft.Web.WebView2.WinForms.WebView2();
            btBack = new Button();
            btForward = new Button();
            btfavorite = new Button();
            btfavoriteDelete = new Button();
            ((System.ComponentModel.ISupportInitialize)wvRssLink).BeginInit();
            SuspendLayout();
            // 
            // cbUrl
            // 
            cbUrl.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            cbUrl.Location = new Point(236, 24);
            cbUrl.Name = "cbUrl";
            cbUrl.Size = new Size(596, 33);
            cbUrl.TabIndex = 0;
            // 
            // btRssGet
            // 
            btRssGet.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btRssGet.Location = new Point(838, 24);
            btRssGet.Name = "btRssGet";
            btRssGet.Size = new Size(75, 33);
            btRssGet.TabIndex = 1;
            btRssGet.Text = "取得";
            btRssGet.UseVisualStyleBackColor = true;
            btRssGet.Click += btRssGet_ClickAsync;
            // 
            // lbTitles
            // 
            lbTitles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lbTitles.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lbTitles.FormattingEnabled = true;
            lbTitles.ItemHeight = 21;
            lbTitles.Location = new Point(12, 114);
            lbTitles.Name = "lbTitles";
            lbTitles.Size = new Size(400, 529);
            lbTitles.TabIndex = 2;
            lbTitles.Click += lbTitles_Click;
            // 
            // wvRssLink
            // 
            wvRssLink.AllowExternalDrop = true;
            wvRssLink.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            wvRssLink.CreationProperties = null;
            wvRssLink.DefaultBackgroundColor = Color.White;
            wvRssLink.Location = new Point(513, 114);
            wvRssLink.Name = "wvRssLink";
            wvRssLink.Size = new Size(508, 529);
            wvRssLink.TabIndex = 4;
            wvRssLink.ZoomFactor = 1D;
            wvRssLink.NavigationCompleted += wvRssLink_NavigationCompleted;
            // 
            // btBack
            // 
            btBack.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btBack.Location = new Point(62, 23);
            btBack.Name = "btBack";
            btBack.Size = new Size(75, 33);
            btBack.TabIndex = 5;
            btBack.Text = "戻る";
            btBack.UseVisualStyleBackColor = true;
            btBack.Click += btBack_Click;
            // 
            // btForward
            // 
            btForward.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btForward.Location = new Point(143, 23);
            btForward.Name = "btForward";
            btForward.Size = new Size(75, 33);
            btForward.TabIndex = 6;
            btForward.Text = "進む";
            btForward.UseVisualStyleBackColor = true;
            btForward.Click += btForward_Click;
            // 
            // btfavorite
            // 
            btfavorite.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btfavorite.Location = new Point(919, 25);
            btfavorite.Name = "btfavorite";
            btfavorite.Size = new Size(102, 32);
            btfavorite.TabIndex = 7;
            btfavorite.Text = "お気に入り";
            btfavorite.UseVisualStyleBackColor = true;
            btfavorite.Click += btfavorite_Click;
            // 
            // btfavoriteDelete
            // 
            btfavoriteDelete.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btfavoriteDelete.Location = new Point(838, 63);
            btfavoriteDelete.Name = "btfavoriteDelete";
            btfavoriteDelete.Size = new Size(183, 36);
            btfavoriteDelete.TabIndex = 8;
            btfavoriteDelete.Text = "お気に入り削除";
            btfavoriteDelete.UseVisualStyleBackColor = true;
            btfavoriteDelete.Click += btfavoriteDelete_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1056, 784);
            Controls.Add(btfavoriteDelete);
            Controls.Add(btfavorite);
            Controls.Add(btForward);
            Controls.Add(btBack);
            Controls.Add(wvRssLink);
            Controls.Add(lbTitles);
            Controls.Add(btRssGet);
            Controls.Add(cbUrl);
            Name = "Form1";
            Text = "RssReader";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)wvRssLink).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox cbUrl;
        private Button btRssGet;
        private ListBox lbTitles;
        private Microsoft.Web.WebView2.WinForms.WebView2 wvRssLink;
        private Button btBack;
        private Button btForward;
        private Button btfavorite;
        private Button btfavoriteDelete;
    }
}
