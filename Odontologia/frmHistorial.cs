using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modelo;

namespace Odontologia
{
    public partial class frmHistorial : Form
    {
        public frmPrincipal fPrincipal;
        public frmHistorial(frmPrincipal fPrincipal)
        {
            this.fPrincipal = fPrincipal;
            InitializeComponent();
        }

        private void mtxtNroDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                dgvHistorial.Rows.Clear();
                Paciente oPaciente = new Paciente();
                oPaciente = oPaciente.GetOne(mtxtNroDocumento.Text);
                lblPaciente.Text = oPaciente.Apellido.ToString().ToUpper() + ", " + oPaciente.Nombre;
                Historial oHistorial = new Historial();
                DataTable oDaTa = oHistorial.GetHistorial(oPaciente.IdPaciente);
                foreach(DataRow fila in oDaTa.Rows)
                {
                    string fecha = Convert.ToDateTime(fila[0].ToString()).ToString("dd-MM-yyyy");
                    string causa = fila[1].ToString();
                    string solucion = fila[2].ToString();
                    string estado = fila[3].ToString();
                    dgvHistorial.Rows.Add(fecha,causa,solucion,estado);
                }
                colorFila();
                dgvHistorial.ClearSelection();
            }
        }
        private void colorFila()
        {
            foreach (DataGridViewRow filaDG in dgvHistorial.Rows)
            {
                switch (filaDG.Cells[3].Value.ToString())
                {
                    case "en espera":
                        filaDG.DefaultCellStyle.BackColor = Color.Orange;
                        break;
                    case "ausente":
                        filaDG.DefaultCellStyle.BackColor = Color.IndianRed;
                        break;
                    case "concretado":
                        filaDG.DefaultCellStyle.BackColor = Color.LightGreen;
                        break;
                }
            }
        }
    }
}
