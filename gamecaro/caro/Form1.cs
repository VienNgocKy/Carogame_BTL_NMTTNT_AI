using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace caro
{
    public partial class Form1 : Form
    {
        BanCo newBanCo;
        QuanLiServer socket;
        public Form1()
        {
            InitializeComponent();
            newBanCo = new BanCo(ptbDaiDien, txtPlayer, prgTimeLine);
            socket = new QuanLiServer();
            prgTimeLine.Maximum = 10000;
            prgTimeLine.Step = 100;
            newBanCo.ChienThang += NewBanCo_ChienThang;
            newBanCo.UndoFalse += NewBanCo_UndoFalse;
            newBanCo.RedoFalse += NewBanCo_RedoFalse;
            newBanCo.PlayerMark += NewBanCo_PlayerMark;
        }

        private void NewBanCo_PlayerMark(object sender, ButtonClickEvent e)
        {
            timer1.Start();
            prgTimeLine.Value = 0;
            if (newBanCo.CheDoChoi == 2)
            {
                try
                {
                    pnlBanCo.Enabled = false;
                    socket.Send(new SocketData((int)SocketCommand.SEND_POINT, "", e.ClickedPoint));
                    Listen();
                }
                catch { }
            }
        }

        private void NewBanCo_RedoFalse(object sender, EventArgs e)
        {
            redoToolStripMenuItem.Enabled = false;
        }

        private void NewBanCo_UndoFalse(object sender, EventArgs e)
        {
            // bắt sự kiện từ BanCo, khóa nút undo
            undoToolStripMenuItem.Enabled = false;
            // done!
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            newBanCo.TaoBanCo(pnlBanCo);
            txtPlayer.Text = "";
            txtPlayer.Enabled = false;
            pnlBanCo.Enabled = false;
            btnConnectLan.Enabled = true;

        }

        private void NewBanCo_ChienThang(object sender, EventArgs e)
        {
            timer1.Stop();
            KetThuc();
        }
        /*
        private void KetThuc()
        {
            pnlBanCo.Enabled = false;
            if (CheDoChoi == 3) { 
            
            };
            MessageBox.Show("Chúc mừng! Người chơi " + newBanCo.ThongTinNguoiThang() + " đã chiến thắng!" );
        }
        */
        private void KetThuc()
        {
            pnlBanCo.Enabled = false;
            string nguoiThang = newBanCo.ThongTinNguoiThang();
            if (newBanCo.CheDoChoi == 3 && nguoiThang.ToLower() == "computer")
            {
                MessageBox.Show("Bạn đã thua!");
            }
            else
            {
                MessageBox.Show("Chúc mừng! Người chơi " + nguoiThang + " đã chiến thắng!");
            }
            if(newBanCo.CheDoChoi!=3) MessageBox.Show("Chúc mừng! Người chơi " + newBanCo.ThongTinNguoiThang() + " đã chiến thắng!");

        }
        private void KetThucTime()
        {
            pnlBanCo.Enabled = false;
            string nguoiThang = newBanCo.ThongTinNguoiThang();
            
            MessageBox.Show("Hết thời gian");

        }


        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }


        private void playerVsPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newBanCo.CheDoChoi = 1;
            nhapten1();
            nhapten2();
            // xóa hết image trong button trên bàn cờ, gọi 
            pnlBanCo.Enabled = true;
            newBanCo.NewGamePvsP();
            undoToolStripMenuItem.Enabled = true;
            redoToolStripMenuItem.Enabled = true;

            // thanh progress không chạy,  mình cần khởi động lại timer1 với progress bar
            timer1.Start();
            prgTimeLine.Value = 0;
        }



        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newBanCo.RedoMethod();
            timer1.Start();
            prgTimeLine.Value = 0;
        }

        private void btnConnectLan_Click(object sender, EventArgs e)
        {
            newBanCo.CheDoChoi = 2;
            nhapten1();
            nhapten2();
            //newBanCo.NguoiChoi1 = new NguoiChoi("Thuy", Image.FromFile(Application.StartupPath + "\\Resources\\CaroX.png"), 1);
            //newBanCo.NguoiChoi2 = new NguoiChoi("Phat", Image.FromFile(Application.StartupPath + "\\Resources\\Caro0.png"), 2);
            socket.IP = txtIP.Text;
            if (!socket.ConnectServer())
            {
                socket.IsServer = true;
                pnlBanCo.Enabled = true;
                socket.CreateServer();
            }
            else
            {
                socket.IsServer = false;
                pnlBanCo.Enabled = false;
                Listen();
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            txtIP.Text = socket.GetLocalIPv4(NetworkInterfaceType.Wireless80211);
            if(string.IsNullOrEmpty(txtIP.Text))
                txtIP.Text = socket.GetLocalIPv4(NetworkInterfaceType.Ethernet);

        }

        void Listen()
        {
            Thread listenThread = new Thread(() =>
            {
                try
                {
                    SocketData data = (SocketData)socket.Receive();

                    ProcessData(data);
                }
                catch
                {
                }
            });
            listenThread.IsBackground = true;
            listenThread.Start();
        }

        private void ProcessData(SocketData Data)
        {
            switch(Data.Command)
            {
                case (int)SocketCommand.SEND_POINT:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        pnlBanCo.Enabled = true;
                        timer1.Start();
                        newBanCo.OtherPlayerMark(Data.Point);
                    }));
                    break;
                case (int)SocketCommand.NOTIFY:
                    MessageBox.Show(Data.Message);
                    break;
                case (int)SocketCommand.NEW_GAME:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        newBanCo.NewGameLAN();
                        prgTimeLine.Value = 0;
                    }));
                    break;
                case (int)SocketCommand.UNDO:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        timer1.Start();
                        prgTimeLine.Value = 0;
                        newBanCo.UndoMethod();
                    }));

                    break;
                case (int)SocketCommand.REDO:
                    break;
                case (int)SocketCommand.EXIT:
                    MessageBox.Show("Người chơi còn lại đã thoát!");
                    break;
                default:
                    break;

            }
            Listen();
        }
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newBanCo.UndoMethod();
            timer1.Start();
            prgTimeLine.Value = 0;
            if (newBanCo.CheDoChoi == 2)
                socket.Send(new SocketData((int)SocketCommand.UNDO, "", new Point()));
        }
        private void playerLANToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnConnectLan.Enabled = true;
            newBanCo.NewGameLAN(); // cho thằng đang bấm new
            socket.Send(new SocketData((int)SocketCommand.NEW_GAME, "", new Point()));// cho đối thủ cũng new theo
            prgTimeLine.Value = 0;
        }


        private void playerVsComputerToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            pnlBanCo.Enabled = true;
            nhapten2();
            newBanCo.NewGamePvPC();
            undoToolStripMenuItem.Enabled = true;
            redoToolStripMenuItem.Enabled = true;
            // thanh progress không chạy,  mình cần khởi động lại timer1 với progress bar
            timer1.Start();
            prgTimeLine.Value = 0;
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            prgTimeLine.PerformStep();
            if (prgTimeLine.Value == prgTimeLine.Maximum)
            {
                timer1.Stop();
                KetThucTime();
            }
                
        }

        private void txtPlayer_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            if (MessageBox.Show("Bạn có chắc muốn thoát?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Application.Exit();
            }

        }
        private void nhapten1()
        {
            string tenNguoiChoi1 = "Người chơi 1";
            using (var form = new Form())
            {
                var textBox = new TextBox() { Left = 50, Top = 50, Width = 150 };
                var label = new Label() { Left = 50, Top = 25, Text = "Nhập tên người chơi 1:" };
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
            newBanCo.NguoiChoi1 = new NguoiChoi(tenNguoiChoi1, Image.FromFile(Application.StartupPath + "\\Resources\\CaroX.png"), 1);

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
            newBanCo.NguoiChoi2 = new NguoiChoi(tenNguoiChoi2, Image.FromFile(Application.StartupPath + "\\Resources\\Caro0.png"), 2);

        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (newBanCo.CheDoChoi == 3) {
                if (MessageBox.Show("Bạn có chắc muốn chơi lại?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    pnlBanCo.Enabled = true;
                    newBanCo.NewGamePvPC();
                    undoToolStripMenuItem.Enabled = true;
                    redoToolStripMenuItem.Enabled = true;
                    // thanh progress không chạy,  mình cần khởi động lại timer1 với progress bar
                    timer1.Start();
                    prgTimeLine.Value = 0;

                }    
                
            }

            if (newBanCo.CheDoChoi == 1)
            {
                timer1.Stop();
                if (MessageBox.Show("Bạn có chắc muốn chơi lại?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    // xóa hết image trong button trên bàn cờ, gọi 
                    pnlBanCo.Enabled = true;
                    newBanCo.NewGamePvsP();
                    undoToolStripMenuItem.Enabled = true;
                    redoToolStripMenuItem.Enabled = true;

                    // thanh progress không chạy,  mình cần khởi động lại timer1 với progress bar
                    timer1.Start();
                    prgTimeLine.Value = 0;

                }
               
            }
            //if (newBanCo.CheDoChoi == 3) { newBanCo.NewGamePvPC(); }


        }
    }
}
