using System;
using System.Drawing;
using System.Windows.Forms;

namespace game
{
    public partial class Load : Form
    {
        Button GameButton = new Button();
        Button InstructionButton = new Button();
        Label GameInstruction = new Label();
        public Load()
        {
            InitializeComponent();
            this.ClientSize = new Size(450, 700);

            
            this.Controls.Add(GameButton);
            GameButton.Text = "Розпочати гру";
            GameButton.Location = new Point(165, 50);
            GameButton.Size = new Size(120, 70);
            GameButton.BackColor = System.Drawing.Color.Azure;
            GameButton.Click += new EventHandler(GameButton_Click);

            
            this.Controls.Add(InstructionButton);
            InstructionButton.Text = "Правила та керування";
            InstructionButton.Location = new Point(165, 150);
            InstructionButton.Size = new Size(120, 70);
            InstructionButton.BackColor = System.Drawing.Color.Azure;
            InstructionButton.Click += new EventHandler(InstructionButton_Click);


            this.Controls.Add(GameInstruction);
            GameInstruction.Text = "\tСтрілка вліво - рух вліво.\n\tСтрілка вправо - рух вправо.\nEsc - пауза.\n\nЗ кожним новим рівнем складність збільшується.\n\t\nЦіль гри: знищити всіх прибульців.\n\n\tГра закінчується коли: \n\n\t- прибульці влучають у гравця.\n\t- прибульці врізаються в гравця.\n\t- прибульці врізаються в захисні блоки.\n\t\nБлоки можуть витримати 5 влучань.";
            GameInstruction.Location = new Point(120, 250);
            GameInstruction.Font = new Font(GameInstruction.Font.FontFamily, 10f);
            GameInstruction.Font = new Font(GameInstruction.Font, GameInstruction.Font.Style ^ FontStyle.Bold);
            GameInstruction.ForeColor = System.Drawing.Color.Yellow;
            GameInstruction.BackColor = Color.Transparent;
            GameInstruction.Size = new Size(220,400);
            GameInstruction.Hide();

            this.FormClosing += new FormClosingEventHandler(FormClose_FormClosing); 
        }

        private void FormClose_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void GameButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            SpaceInvaders game = new SpaceInvaders();
            game.Show();
        }


        bool isInstructionVisible = false;
        private void InstructionButton_Click(object sender, EventArgs e)
        {
            isInstructionVisible = !isInstructionVisible;

            if (isInstructionVisible)
            {
                GameInstruction.Show();
            }
            else
            {
                GameInstruction.Hide();
            }
        }
    }
}
