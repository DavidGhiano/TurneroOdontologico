using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modelo;

namespace Odontologia
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
            Turno oTurno = new Turno();
            DateTime yesterday = DateTime.Today.AddDays(-1);
            oTurno.ActualizarEstadoCita(yesterday);
            pnlCentral.Dock = DockStyle.Fill;
            AbrirFormHijo(new frmInicio(this));

        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void pnlTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xF012, 0);
        }

        private void AbrirFormHijo(object formHijo)
        {
            if(this.pnlCentral.Controls.Count > 0)
            {
                this.pnlCentral.Controls.RemoveAt(0);
            }
            Form formH = formHijo as Form;
            formH.TopLevel = false;
            formH.Dock = DockStyle.Fill;
            this.pnlCentral.Controls.Add(formH);
            this.pnlCentral.Tag = formH;
            formH.Show();
            this.pnlCentral.Visible = true;
        }


        private void pbxCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pbxMaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            pbxMaximizar.Visible = false;
            pbxNormalizar.Visible = true;
        }

        private void pbxNormalizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            pbxMaximizar.Visible = true;
            pbxNormalizar.Visible = false;
        }

        private void pbxMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pbxCerrar_MouseHover(object sender, EventArgs e)
        {
            this.pbxCerrar.BackColor = Color.Red;
        }
        private void pbxCerrar_MouseLeave(object sender, EventArgs e)
        {
            this.pbxCerrar.BackColor = Color.FromArgb(36, 47, 61);
        }

        private void pbxNormalizar_MouseHover(object sender, EventArgs e)
        {
            this.pbxNormalizar.BackColor = Color.FromArgb(43, 82, 120);
        }

        private void pbxNormalizar_MouseLeave(object sender, EventArgs e)
        {
            this.pbxNormalizar.BackColor = Color.FromArgb(36, 47, 61);
        }

        private void pbxMaximizar_MouseHover(object sender, EventArgs e)
        {
            this.pbxMaximizar.BackColor = Color.FromArgb(43, 82, 120);
        }

        private void pbxMaximizar_MouseLeave(object sender, EventArgs e)
        {
            this.pbxMaximizar.BackColor = Color.FromArgb(36, 47, 61);
        }

        private void pbxMinimizar_MouseHover(object sender, EventArgs e)
        {
            this.pbxMinimizar.BackColor = Color.FromArgb(43, 82, 120);
        }

        private void pbxMinimizar_MouseLeave(object sender, EventArgs e)
        {
            this.pbxMinimizar.BackColor = Color.FromArgb(36, 47, 61);
        }

        private void btnPacientes_Click(object sender, EventArgs e)
        {
            AbrirFormHijo(new frmPaciente(this));
        }

        private void pbxLogo_Click(object sender, EventArgs e)
        {
            AbrirFormHijo(new frmInicio(this));
        }

        private void btnTurnos_Click(object sender, EventArgs e)
        {
            AbrirFormHijo(new frmTurno(this));
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            AbrirFormHijo(new frmInicio(this));
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            AbrirFormHijo(new frmHistorial(this));
        }
    }
}
