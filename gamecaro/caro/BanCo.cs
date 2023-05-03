using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace caro
{
    public class BanCo
    {
        #region Properties
        public Stack<Button> dsRedo { get; set; }
        Stack<Button> dsUndo;
        public int LuotChoi;
        PictureBox ptb1;
        TextBox txt1;
        ProgressBar prg;
        public NguoiChoi NguoiChoi1 { get; set; }
        public NguoiChoi NguoiChoi2 { get; set; }
        private event EventHandler chienThang;
        public event EventHandler ChienThang
        {
            add
            {
                chienThang += value;
                
            }
            remove
            {
                chienThang += value;
            }
        }
        private event EventHandler<ButtonClickEvent> playerMark;
        public event EventHandler<ButtonClickEvent> PlayerMark
        {
            add
            {
                playerMark += value;

            }
            remove
            {
                playerMark += value;
            }
        }
        private event EventHandler undoFalse;
        public event EventHandler UndoFalse
        {
            add
            {
                undoFalse += value;
            }
            remove
            {
                undoFalse += value;
            }
        }
        private event EventHandler redoFalse;
        public event EventHandler RedoFalse
        {
            add
            {
                redoFalse += value;
            }
            remove
            {
                redoFalse += value;
            }
        }
        List<List<Button>> ArroCo;
        int Dem1;
        int Dem2;
        public int CheDoChoi;
        public BanCo(PictureBox ptb, TextBox txt,ProgressBar prg1)
        {
            CheDoChoi = 2;
            //nhapten1();
            //nhapten2();
            NguoiChoi1 = new NguoiChoi("",Image.FromFile(Application.StartupPath + "\\Resources\\CaroX.png"),1);
            NguoiChoi2 = new NguoiChoi("", Image.FromFile(Application.StartupPath + "\\Resources\\Caro0.png"),2);
            LuotChoi = 1;
            ptb1 = ptb;
            txt1 = txt;
            prg = prg1;
            dsUndo = new Stack<Button>();
            dsRedo = new Stack<Button>();
        }
        #endregion

        #region Methods
        public void TaoBanCo(Panel pnlBanCo)
        {
            Button btn = new Button();
            btn.Size = new Size(0, 0);
            ArroCo = new List<List<Button>>();
            for (int i = 0; i < 15; i++)
            {
                ArroCo.Add(new List<Button>()); // tạo ra 1 hàng
                for (int j = 0; j < 15; j++)
                {

                    Button btn1 = new Button();
                    btn1.Size = new Size(30,30);
                    btn1.Location = new Point(btn.Width + btn.Location.X, btn.Location.Y);
                    btn = btn1;
                    btn.Tag = i.ToString();
                    ArroCo[i].Add(btn);

                    btn.BackgroundImageLayout = ImageLayout.Stretch;
                    btn.Click += Btn_Click;
                    pnlBanCo.Controls.Add(btn);
                }
                btn = new Button()
                { Size = new Size(0, 0), Location = new Point(0, btn.Location.Y + btn.Height) };
            }
            ThongTinLuotDi();
        }

        private Point GetChessPoint(Button btn)
        {
            int Y = Convert.ToInt32(btn.Tag);
            int X = ArroCo[Y].IndexOf(btn);

            Point point = new Point(X,Y);

            return point;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button newbtn = sender as Button;
            if (newbtn.BackgroundImage != null)
                return;
            if(LuotChoi==NguoiChoi1.NgChoi)
                newbtn.BackgroundImage = NguoiChoi1.HinhDaiDien;
            else
                newbtn.BackgroundImage = NguoiChoi2.HinhDaiDien;
            if(playerMark!=null)
            {
                playerMark(this, new ButtonClickEvent(GetChessPoint(newbtn)));
            }
            if (KiemTraThangThua(newbtn))
            {
                chienThang(this, new EventArgs());
                return;
            }
            dsUndo.Push(newbtn);
            LuotChoi = LuotChoi == 1 ? 2 : 1;
            ThongTinLuotDi();
            prg.Value = 0;
            if (CheDoChoi == 3 && LuotChoi == 1)
                ComChess();
        }
        private Point ToaDoCo(Button btn)
        {
            Point pnt;
            int ToaDoY = Convert.ToInt32(btn.Tag);
            int ToaDoX = ArroCo[ToaDoY].IndexOf(btn);
            pnt = new Point(ToaDoX, ToaDoY);
            return pnt;
        }
        private void ThongTinLuotDi()
        {
            if(LuotChoi==NguoiChoi1.NgChoi)
            {
                ptb1.Image = NguoiChoi1.HinhDaiDien;
                txt1.Text = NguoiChoi1.Ten;
            }
            else
            {
                ptb1.Image = NguoiChoi2.HinhDaiDien;
                txt1.Text = NguoiChoi2.Ten;
            }
        }
        #region Kiểm tra thắng thua - thông tin người thắng
        private bool KiemTraThangThua(Button btn)
        {

            return ThangHangDoc(btn) || ThangHangNgang(btn) || ThangCheoChinh(btn) || ThangCheoPhu(btn);
        }

        private bool ThangCheoPhu(Button btn)
        {
            Point ToaDoCoHienTai = ToaDoCo(btn);
            Dem1 = 0;
            //Đếm bên trai duoi
            int Dem = 0;
            for (int i = ToaDoCoHienTai.X - 1; i >= 0; i--)
            {
                if(ToaDoCoHienTai.Y+Dem<ArroCo.Count-1)
                {
                    Dem++;
                    if (ArroCo[ToaDoCoHienTai.Y + Dem][i].BackgroundImage == btn.BackgroundImage)
                    {
                        Dem1++;
                        if (Dem1 == 4)
                            return true;
                    }
                    else
                        break;
                }
            }
            //Đếm bên phải tren
            Dem2 = 0;
            Dem = 0;
            for (int i = ToaDoCoHienTai.X + 1; i < ArroCo[ToaDoCoHienTai.Y].Count; i++)
            {
                if (ToaDoCoHienTai.Y - Dem >0)
                {
                    Dem++;
                    if (ArroCo[ToaDoCoHienTai.Y - Dem][i].BackgroundImage == btn.BackgroundImage)
                    {
                        Dem2++;
                        if (Dem2 == 4)
                            return true;
                    }
                    else
                        break;
                }
            }
            if (Dem1 + Dem2 + 1 == 5)
                return true;
            return false;
        }

        private bool ThangCheoChinh(Button btn)
        {
            Point ToaDoCoHienTai = ToaDoCo(btn);
            Dem1 = 0;
            //Đếm bên trái tren
            int Dem = 0;
            for (int i = ToaDoCoHienTai.X - 1; i >= 0; i--)
            {
                Dem++;
                if ((ToaDoCoHienTai.Y - Dem) > 0)
                {
                    if (ArroCo[ToaDoCoHienTai.Y - Dem][i].BackgroundImage == btn.BackgroundImage)
                    {
                        Dem1++;
                        if (Dem1 == 4)
                            return true;
                    }
                    else
                        break;
                }
            }
            //Đếm bên phải duoi
            Dem2 = 0;
            Dem = 0;
            for (int i = ToaDoCoHienTai.X + 1; i < ArroCo[ToaDoCoHienTai.Y].Count; i++)
            {

                if(ToaDoCoHienTai.Y+Dem<ArroCo.Count-1)
                {
                    Dem++;
                    if (ArroCo[ToaDoCoHienTai.Y + Dem][i].BackgroundImage == btn.BackgroundImage)
                    {
                        Dem2++;
                        if (Dem2 == 4)
                            return true;
                    }
                    else
                        break;
                }
            }
            if (Dem1 + Dem2 + 1 == 5)
                return true;
            return false;
        }

        private bool ThangHangNgang(Button btn)
        {
            Point ToaDoCoHienTai  = ToaDoCo(btn);
            Dem1 = 0;
            //Đếm bên trái
            for(int i=ToaDoCoHienTai.X - 1;i >=0;i--)
            {
                if (ArroCo[ToaDoCoHienTai.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    Dem1++;
                    if (Dem1 == 4)
                        return true;
                }
                else
                    break;

            }
            //Đếm bên phải
            Dem2 = 0;
            for (int i = ToaDoCoHienTai.X + 1; i < ArroCo[ToaDoCoHienTai.Y].Count; i++)
            {
                if (ArroCo[ToaDoCoHienTai.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    Dem2++;
                    if (Dem2 == 4)
                        return true;
                }
                else
                    break;
            }
            if (Dem1 + Dem2 + 1 == 5)
                return true;
            return false;
        }

        private bool ThangHangDoc(Button btn)
        {
            Point ToaDoCoHienTai = ToaDoCo(btn);
            Dem1 = 0;
            //Đếm bên tren
            for (int i = ToaDoCoHienTai.Y - 1; i >= 0; i--)
            {
                if (ArroCo[i][ToaDoCoHienTai.X].BackgroundImage == btn.BackgroundImage)
                {
                    Dem1++;
                    if (Dem1 == 4)
                        return true;
                }
                else
                    break;

            }
            //Đếm bên duoi
            Dem2 = 0;
            for (int i = ToaDoCoHienTai.Y + 1; i < ArroCo.Count; i++)
            {
                if (ArroCo[i][ToaDoCoHienTai.X].BackgroundImage == btn.BackgroundImage)
                {
                    Dem2++;
                    if (Dem2 == 4)
                        return true;
                }
                else
                    break;
            }
            if (Dem1 + Dem2 + 1 == 5)
                return true;
            return false;
        }

        public string ThongTinNguoiThang()
        {
            return LuotChoi == NguoiChoi1.NgChoi ? NguoiChoi1.Ten : NguoiChoi2.Ten;
        }
        #endregion
        public void NewGamePvsP()
        {// cho chạy 2 vòng lập duyệt toàn bộ button trong List<list<btn>>

            for(int i=0; i <ArroCo.Count;i++)
            {
                for(int j=0; j < ArroCo[i].Count;j++)
                {
                    // đổi background image
                    ArroCo[i][j].BackgroundImage = null;
                }
                // chuyển về lượt người đầu tiên
                LuotChoi = 1;
                ThongTinLuotDi();
                dsRedo = new Stack<Button>();
                dsUndo = new Stack<Button>();
            }
            // MessageBox.Show("Máy đánh trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MessageBox.Show(NguoiChoi1.Ten + " sẽ đi trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        public void UndoMethod()
        {
            Button undobtn = dsUndo.Pop();
            // mỗi lần undo thành công ta lưu lại trong ds redo
            dsRedo.Push(undobtn);
            undobtn.BackgroundImage = null;
            LuotChoi = LuotChoi == 1 ? 2 : 1;
            ThongTinLuotDi();
            if (dsUndo.Count == 0) // tạo sự kiện nhả cho form
            {
                undoFalse(this, new EventArgs());
                return;
            }
        }
        public void RedoMethod()
        {
            Button redobtn = dsRedo.Pop();
            if (LuotChoi == 1)
                redobtn.BackgroundImage = NguoiChoi1.HinhDaiDien;
            else
                redobtn.BackgroundImage = NguoiChoi2.HinhDaiDien;
            LuotChoi = LuotChoi == 1 ? 2 : 1;
            ThongTinLuotDi();
            if (dsRedo.Count == 0)
            {
                redoFalse(this, new EventArgs());
                return;
            }
        }
        public void NewGameLAN()
        {// cho chạy 2 vòng lập duyệt toàn bộ button trong List<list<btn>>

            for (int i = 0; i < ArroCo.Count; i++)
            {
                for (int j = 0; j < ArroCo[i].Count; j++)
                {
                    // đổi background image
                    ArroCo[i][j].BackgroundImage = null;
                }
                // chuyển về lượt người đầu tiên
                LuotChoi = 1;
                ThongTinLuotDi();
                dsRedo = new Stack<Button>();
                dsUndo = new Stack<Button>();
            }
        }
        public void OtherPlayerMark(Point point)
        {
            Button newbtn = ArroCo[point.Y][point.X];
            if (newbtn.BackgroundImage != null)
                return;
            if (LuotChoi == NguoiChoi1.NgChoi)
                newbtn.BackgroundImage = NguoiChoi1.HinhDaiDien;
            else
                newbtn.BackgroundImage = NguoiChoi2.HinhDaiDien;
            if (KiemTraThangThua(newbtn))
            {
                chienThang(this, new EventArgs());
                return;
            }
            dsUndo.Push(newbtn);
            LuotChoi = LuotChoi == 1 ? 2 : 1;
            ThongTinLuotDi();
            prg.Value = 0;

        }

        public void NewGamePvPC()// chế độ chơi giữa người với máy

        {
            CheDoChoi = 3;
           
            for (int i = 0; i < ArroCo.Count; i++)
            {
                for (int j = 0; j < ArroCo[i].Count; j++)
                {
                    // đổi background image
                    ArroCo[i][j].BackgroundImage = null;
                }
                // chuyển về lượt người đầu tiên
                LuotChoi = 1;
                ThongTinLuotDi();
                dsRedo = new Stack<Button>();
                dsUndo = new Stack<Button>();
                NguoiChoi1 = new NguoiChoi("Computer", Image.FromFile(Application.StartupPath + "\\Resources\\CaroX.png"), 1);// Chọn mặc định cho máy tính đi trước
               

                
            }
            MessageBox.Show("Máy đánh trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information,
                 MessageBoxDefaultButton.Button1, 0, Application.StartupPath + "\\Resources\\CaroX.png");
            ComChess();
        }
        private void nhapten2()
        {
            string tenNguoiChoi2 = "Người chơi 2";
            using (var form = new Form())
            {
                var textBox = new TextBox() { Left = 50, Top = 50, Width = 150 };
                var label = new Label() { Left = 50, Top = 25, Text = "Nhập tên người chơi 2:" };
                var buttonOk = new Button() { Text = "OK", Left = 100, Width = 50, Top = 80 };
                buttonOk.Click += (s, d) => { form.DialogResult = DialogResult.OK; };
                form.Controls.Add(label);
                form.Controls.Add(textBox);
                form.Controls.Add(buttonOk);
                form.AcceptButton = buttonOk;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    tenNguoiChoi2 = textBox.Text;
                }
            }

            // Tạo đối tượng NguoiChoi với tên đã nhập
            NguoiChoi2 = new NguoiChoi(tenNguoiChoi2, Image.FromFile(Application.StartupPath + "\\Resources\\Caro0.png"), 2);

        }
        private void nhapten1()
        {
            string tenNguoiChoi1 = "Người chơi 1";
            using (var form = new Form())
            {
                var textBox = new TextBox() { Left = 50, Top = 50, Width = 150 };
                var label = new Label() { Left = 50, Top = 25, Text = "Nhập tên người chơi 2:" };
                var buttonOk = new Button() { Text = "OK", Left = 100, Width = 50, Top = 80 };
                buttonOk.Click += (s, d) => { form.DialogResult = DialogResult.OK; };
                form.Controls.Add(label);
                form.Controls.Add(textBox);
                form.Controls.Add(buttonOk);
                form.AcceptButton = buttonOk;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    tenNguoiChoi1 = textBox.Text;
                }
            }

            // Tạo đối tượng NguoiChoi với tên đã nhập
            NguoiChoi1 = new NguoiChoi(tenNguoiChoi1, Image.FromFile(Application.StartupPath + "\\Resources\\Caro0.png"), 2);

        }
        private long[] arrAttackPoint = { 0, 6, 48, 384, 3072, 24576, 196608 };

        // private long[] arrAttackPoint = { 0, 3, 24, 192, 1536, 12288, 98304 };
        private long[] arrDefendPoint = { 0, 1, 9, 81, 729, 6561, 59049 };


        private void randomstart()
        {
            Random rnd = new Random();
            int randomNum = rnd.Next(1, 3);
            if (randomNum == 1)
            {
                ComChess();// lấy từ hàm pcsPC xuống
                NguoiChoi1 = new NguoiChoi("Computer", Image.FromFile(Application.StartupPath + "\\Resources\\CaroX.png"), 1);
                NguoiChoi2 = new NguoiChoi("Phat", Image.FromFile(Application.StartupPath + "\\Resources\\Caro0.png"),2);

            }
            else
            {
                NguoiChoi1 = new NguoiChoi("Phat", Image.FromFile(Application.StartupPath + "\\Resources\\Caro0.png"), 2);

                //                NguoiChoi1 = new NguoiChoi("Player 2", Image.FromFile(Application.StartupPath + "\\Resources\\CaroO.png"), 2);
                NguoiChoi2 = new NguoiChoi("Computer", Image.FromFile(Application.StartupPath + "\\Resources\\CaroX.png"), 1);
            }
            MessageBox.Show(NguoiChoi1.Ten + " sẽ đi trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }


        private void ComChess()// Chọn nước đánh đầu tiên cho máy tính
        {
            if(dsUndo.Count == 0)   
            {
                Btn_Click(ArroCo[ArroCo.Count/2][ArroCo[0].Count/2], new EventArgs());
               
            }
            else
            {
                Button Shouldchess = SearchForChess();
                Point x = GetChessPoint(Shouldchess);
                Btn_Click(Shouldchess, new EventArgs());
            }

        }

        private Button SearchForChess()
        {
            Button Chess = new Button();
            long Temp = 0;
            long DiemTC = 0;
            long DiemPT = 0;
            long DiemMAX = 0;
            for (int i = 0; i < ArroCo.Count; i++)
            {
                for (int j = 0; j < ArroCo[i].Count; j++)
                {
                    if (ArroCo[i][j].BackgroundImage == null)
                    {
                        DiemTC = DiemTCDoc(i, j) + DiemTCNgang(i, j) + DiemTCCheoChinh(i, j) + DiemTCCheoPhu(i, j);
                        DiemPT = DiemPTDoc(i, j) + DiemPTNgang(i, j) + DiemPTCheoChinh(i, j) + DiemPTCheoPhu(i, j);
                        Temp = DiemTC > DiemPT ? DiemTC : DiemPT;
                        if (Temp > DiemMAX)
                        {
                            Chess = ArroCo[i][j];
                            DiemMAX = Temp;
                        }
                    }
                }
            }
            return Chess;
        }

        private long DiemTCCheoPhu(int r, int c)
        {
            long Sum = 0;
            int enemy = 0;
            int teammate = 0;

            // Tính điểm theo hướng chéo phụ xuống dưới
            for (int count = 1; count < 6 && r + count < ArroCo.Count && c - count >= 0; count++)
            {
                if (ArroCo[r + count][c - count].BackgroundImage == NguoiChoi1.HinhDaiDien)
                    teammate++;
                else if (ArroCo[r + count][c - count].BackgroundImage == NguoiChoi2.HinhDaiDien)
                {
                    enemy++;
                    break;
                }
                else
                    break;
            }

            // Tính điểm theo hướng chéo phụ lên trên
            for (int count = 1; count < 6 && r - count >= 0 && c + count < ArroCo.Count; count++)
            {
                if (ArroCo[r - count][c + count].BackgroundImage == NguoiChoi1.HinhDaiDien)
                    teammate++;
                else if (ArroCo[r - count][c + count].BackgroundImage == NguoiChoi2.HinhDaiDien)
                {
                    enemy++;
                    break;
                }
                else
                    break;
            }

            Sum -= arrDefendPoint[enemy + 1];
            Sum += arrAttackPoint[teammate];

            return Sum;
        }


        private long DiemTCCheoChinh(int r, int c)
        {
            long Sum = 0;
            int enemy = 0;
            int teammate = 0;

            // Tìm số lượng quân đồng đội và đối thủ trên đường chéo chính phía trên ô hiện tại
            for (int count = 1; count < 6 && r - count >= 0 && c - count >= 0; count++)
            {
                if (ArroCo[r - count][c - count].BackgroundImage == NguoiChoi1.HinhDaiDien)
                    teammate++;
                else if (ArroCo[r - count][c - count].BackgroundImage == NguoiChoi2.HinhDaiDien)
                {
                    enemy++;
                    break;
                }
                else
                    break;
            }

            // Tìm số lượng quân đồng đội và đối thủ trên đường chéo chính phía dưới ô hiện tại
            for (int count = 1; count < 6 && r + count < ArroCo.Count && c + count < ArroCo[r + count].Count; count++)
            {
                if (ArroCo[r + count][c + count].BackgroundImage == NguoiChoi1.HinhDaiDien)
                    teammate++;
                else if (ArroCo[r + count][c + count].BackgroundImage == NguoiChoi2.HinhDaiDien)
                {
                    enemy++;
                    break;
                }
                else
                    break;
            }

            // Tính điểm dựa trên số lượng quân đồng đội và đối thủ trên đường chéo chính
            Sum -= arrDefendPoint[enemy + 1];
            Sum += arrAttackPoint[teammate];

            return Sum;
        }


        private long DiemTCNgang(int r, int c)
        {
            long Sum = 0;
            int enemy = 0;
            int teammate = 0;
            for (int count = 1; count < 6 && c + count < ArroCo[0].Count; count++)
            {
                if (ArroCo[r][c + count].BackgroundImage == NguoiChoi1.HinhDaiDien)
                    teammate++;
                else if (ArroCo[r][c + count].BackgroundImage == NguoiChoi2.HinhDaiDien)
                {
                    enemy++;
                    break;
                }
                else
                    break;
            }
            /////
            for (int count = 1; count < 6 && c - count >= 0; count++)
            {
                if (ArroCo[r][c - count].BackgroundImage == NguoiChoi1.HinhDaiDien)
                    teammate++;
                else if (ArroCo[r][c - count].BackgroundImage == NguoiChoi2.HinhDaiDien)
                {
                    enemy++;
                    break;
                }
                else
                    break;
            }
            Sum -= arrDefendPoint[enemy + 1];
            Sum += arrAttackPoint[teammate];

            return Sum;

        }

        private long DiemTCDoc(int r, int c)
        {
            long Sum = 0;
            int enemy = 0;
            int teammate = 0;
            for (int count = 1; count < 6 && r + count < ArroCo.Count; count++)  
            {
                if (ArroCo[r + count][c].BackgroundImage == NguoiChoi1.HinhDaiDien)
                    teammate++;
                else if (ArroCo[r + count][c].BackgroundImage == NguoiChoi2.HinhDaiDien)
                { 
                    enemy++;
                    break;
                }
                else
                    break;
            }
            /////
            for (int count = 1; count < 6 && r - count >= 0; count++)
            {
                if (ArroCo[r - count][c].BackgroundImage == NguoiChoi1.HinhDaiDien)
                    teammate++;
                else if (ArroCo[r- count][c].BackgroundImage == NguoiChoi2.HinhDaiDien)
                {
                    enemy++;
                    break;
                }
                else
                    break;
            }
            Sum -= arrDefendPoint[enemy + 1];
            Sum += arrAttackPoint[teammate];

            return Sum;
        }

        private long DiemPTCheoPhu(int r, int c)
        {
            long Sum = 0;
            int enemy = 0;
            int teammate = 0;
            for (int count = 1; count < 6 && r + count < ArroCo.Count && c - count >= 0; count++)
            {
                if (ArroCo[r + count][c - count].BackgroundImage == NguoiChoi1.HinhDaiDien)
                {
                    teammate++;
                    break;
                }
                else if (ArroCo[r + count][c - count].BackgroundImage == NguoiChoi2.HinhDaiDien)
                {
                    enemy++;
                }
                else
                    break;
            }
            /////
            for (int count = 1; count < 6 && r - count >= 0 && c + count < ArroCo[0].Count; count++)
            {
                if (ArroCo[r - count][c + count].BackgroundImage == NguoiChoi1.HinhDaiDien)
                {
                    teammate++;
                    break;
                }
                else if (ArroCo[r - count][c + count].BackgroundImage == NguoiChoi2.HinhDaiDien)
                {
                    enemy++;
                }
                else
                    break;
            }
            Sum += arrDefendPoint[enemy + 2];
            return Sum;
        }



        private long DiemPTCheoChinh(int r, int c)
        {
            long Sum = 0;
            int enemy = 0;
            int teammate = 0;

            // Kiểm tra hướng đường chéo chính bên phải của ô hiện tại
            for (int count = 1; count < 6 && r + count < ArroCo.Count && c + count < ArroCo[0].Count; count++)
            {
                if (ArroCo[r + count][c + count].BackgroundImage == NguoiChoi1.HinhDaiDien)
                {
                    teammate++;
                    break;
                }
                else if (ArroCo[r + count][c + count].BackgroundImage == NguoiChoi2.HinhDaiDien)
                {
                    enemy++;
                }
                else
                    break;
            }
            // Kiểm tra hướng đường chéo chính bên trái của ô hiện tại
            for (int count = 1; count < 6 && r - count >= 0 && c - count >= 0; count++)
            {
                if (ArroCo[r - count][c - count].BackgroundImage == NguoiChoi1.HinhDaiDien)
                {
                    teammate++;
                    break;
                }
                else if (ArroCo[r - count][c - count].BackgroundImage == NguoiChoi2.HinhDaiDien)
                {
                    enemy++;
                }
                else
                    break;
            }

            Sum += arrDefendPoint[enemy + 2];
            return Sum;
        }


        private long DiemPTNgang(int r, int c)
        {
            long Sum = 0;
            int enemy = 0;
            int teammate = 0;
            for (int count = 1; count < 6 && c + count < ArroCo[0].Count; count++)
            {
                if (ArroCo[r][c + count].BackgroundImage == NguoiChoi1.HinhDaiDien)
                {
                    teammate++;
                    break;
                }
                else if (ArroCo[r][c + count].BackgroundImage == NguoiChoi2.HinhDaiDien)
                {
                    enemy++;
                }
                else
                    break;
            }
            /////
            for (int count = 1; count < 6 && c - count >= 0; count++)
            {
                if (ArroCo[r][c - count].BackgroundImage == NguoiChoi1.HinhDaiDien)
                {
                    teammate++;
                    break;
                }
                else if (ArroCo[r][c - count].BackgroundImage == NguoiChoi2.HinhDaiDien)
                {
                    enemy++;
                }
                else
                    break;
            }
            Sum += arrDefendPoint[enemy + 2];
            return Sum;

        }

        private long DiemPTDoc(int r, int c)
        {
            long Sum = 0;
            int enemy = 0;
            int teammate = 0;
            for (int count = 1; count < 6 && r + count < ArroCo.Count; count++)
            {
                if (ArroCo[r + count][c].BackgroundImage == NguoiChoi1.HinhDaiDien)
                {
                    teammate++;
                    break;
                }
                else if (ArroCo[r + count][c].BackgroundImage == NguoiChoi2.HinhDaiDien)
                {
                    enemy++;
                }
                else
                    break;
            }
            /////
            for (int count = 1; count < 6 && r - count >= 0; count++)
            {
                if (ArroCo[r - count][c].BackgroundImage == NguoiChoi1.HinhDaiDien)
                {
                    teammate++;
                    break;
                }

                else if (ArroCo[r - count][c].BackgroundImage == NguoiChoi2.HinhDaiDien)
                {
                    enemy++;
                }
                else
                    break;
            }
            Sum += arrDefendPoint[enemy + 2];
            return Sum;
        }

        #endregion
    }
    public class ButtonClickEvent : EventArgs
    {
        public Point ClickedPoint { get; set; }

        public ButtonClickEvent(Point point)
        {
            this.ClickedPoint = point;
        }
    }
}
