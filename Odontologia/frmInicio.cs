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
    public partial class frmInicio : Form
    {
        public frmPrincipal fPrincipal;
        public frmInicio(frmPrincipal _fPrincipal)
        {
            InitializeComponent();
            this.fPrincipal = _fPrincipal;
            cargarDataGrid(DateTime.Today);

        }

        private void tmrReloj_Tick(object sender, EventArgs e)
        { 
            lblHora.Text = DateTime.Now.ToLongTimeString();
            lblFecha.Text = DateTime.Now.ToLongDateString();
        }
        private void cargarDataGrid(DateTime fecha)
        {

            dgvHorario.Rows.Add("09:00");
            dgvHorario.Rows.Add("09:30");
            dgvHorario.Rows.Add("10:00");
            dgvHorario.Rows.Add("10:30");
            dgvHorario.Rows.Add("11:00");
            dgvHorario.Rows.Add("11:30");
            dgvHorario.Rows.Add("12:00");
            dgvHorario.Rows.Add("12:30");
            dgvHorario.Rows.Add("14:00");
            dgvHorario.Rows.Add("14:30");
            dgvHorario.Rows.Add("15:00");
            dgvHorario.Rows.Add("15:30");
            dgvHorario.Rows.Add("16:00");
            dgvHorario.Rows.Add("16:30");
            dgvHorario.Rows.Add("17:00");
            Turno oTurno = new Turno();
            DataTable oDaTa = oTurno.ObtenerTurnoDelDia(fecha);
            foreach (DataRow filas in oDaTa.Rows)
            {
                foreach (DataGridViewRow filaDG in dgvHorario.Rows)
                {
                    if (filaDG.Cells[0].Value.ToString() == filas["Hora"].ToString())
                    {
                        filaDG.Cells[1].Value = filas["Paciente"].ToString();
                        filaDG.Cells[2].Value = filas["idPaciente"].ToString();
                        filaDG.Cells[3].Value = filas["idTurno"].ToString();
                        filaDG.Cells[4].Value = filas["idHistorial"].ToString();
                        filaDG.Cells[5].Value = filas["estado"].ToString();
                    }
                }
            }
        }

        private void dgvHorario_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvHorario.CurrentRow.Cells[1].Value == null)
                {
                    dgvHorario.ClearSelection();
                    pnlInfo.Size = new Size(0, 562);
                    vaciarCampos();
                }
                else
                {
                    int idHistorial = Convert.ToInt32(dgvHorario.CurrentRow.Cells["colIdHistorial"].Value);
                    Historial oHistorial = new Historial();
                    oHistorial = oHistorial.GetHistorialTurno(idHistorial);
                    lblMotivo.Text = oHistorial.Causa;
                    txtDiagnostico.Text = oHistorial.Solucion;
                    if(txtDiagnostico.Text != "")
                    {
                        btnRegistrar.Text = "Actualizar Cita";
                        btnRegistrar.Enabled = true;
                    }
                    pnlInfo.Size = new Size(188, 562);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void vaciarCampos()
        {
            txtDiagnostico.Text = "";
            lblMotivo.Text = "";
            btnRegistrar.Text = "Concretar Cita";
            pnlInfo.Size = new Size(0, 562);
            btnRegistrar.Enabled = false;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            Historial oHistorial = new Historial();
            Turno oTurno = new Turno();

            oHistorial.IdHistorial = Convert.ToInt32(dgvHorario.CurrentRow.Cells["colIdHistorial"].Value);
            oHistorial.Solucion = txtDiagnostico.Text;
            oHistorial.ActualizarHistorial(oHistorial);

            int idTurno = Convert.ToInt32(dgvHorario.CurrentRow.Cells["colIdTurno"].Value);
            oTurno.ActualizarEstadoCita(idTurno);


            vaciarCampos();
            cargarDataGrid(DateTime.Today);
        }

        private void txtDiagnostico_TextChanged(object sender, EventArgs e)
        {
            btnRegistrar.Enabled = true;
        }
    }
}
