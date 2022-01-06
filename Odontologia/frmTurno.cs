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
    public partial class frmTurno : Form
    {
        public frmPrincipal fPrincipal;
        public frmTurno(frmPrincipal fPrincipal)
        {
            InitializeComponent();
            this.fPrincipal = fPrincipal;
            cargarDataGrid(DateTime.Today);
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
                    if (filaDG.Cells[0].Value.ToString() == filas[0].ToString())
                    {
                        filaDG.Cells[1].Value = filas[1].ToString();
                    }
                }
            }
        }

        private void mtCalendario_DateSelected(object sender, DateRangeEventArgs e)
        {
            dgvHorario.Rows.Clear();
            cargarDataGrid(mtCalendario.SelectionStart);

        }

        private void mtxtNroDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((int)e.KeyChar == (int)Keys.Enter)
            {
                Paciente oPaciente = new Paciente();
                oPaciente = oPaciente.GetOne(mtxtNroDocumento.Text);
                if(oPaciente != null)
                {
                    lblIdPaciente.Text = oPaciente.IdPaciente.ToString();
                    lblPaciente.Text = oPaciente.Nombre + " " + oPaciente.Apellido;
                    lblEdad.Text = oPaciente.CalcularEdad().ToString() + " Años";
                    lblTelefono.Text = oPaciente.Telefono;
                    validarCampos();
                }
                else
                {
                    lblIdPaciente.Text = "";
                    lblPaciente.Text = "Paciente no registrado";
                    lblEdad.Text = "";
                    lblTelefono.Text = "";
                }
                
            }
        }

        private void dgvHorario_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvHorario.CurrentRow.Cells[1].Value != null)
                {
                    dgvHorario.ClearSelection();
                }
                validarCampos();
                
            }
            catch (Exception ex)
            {

                MessageBox.Show( ex.Message);
            }
            
        }
        private void validarCampos()
        {
            if(dgvHorario.CurrentRow.Cells[1].Value == null && txtCausa.Text != "" && lblIdPaciente.Text != "")
            {
                btnRegistrar.Enabled = true;
            }
            else
            {
                btnRegistrar.Enabled = false;
            }
        }

        private void txtCausa_TextChanged(object sender, EventArgs e)
        {
            validarCampos();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            Historial oHistoria = new Historial();
            Turno oTurno = new Turno();
            //registro primero el historial
            oHistoria.Causa = txtCausa.Text;
            oHistoria.IdPaciente = Convert.ToInt32( lblIdPaciente.Text);
            lblIdHistorial.Text = oHistoria.RegistrarNuevoHistorial(oHistoria).ToString() ;
            //luego registro el turno
            oTurno.Fecha = mtCalendario.SelectionStart;
            oTurno.Hora = dgvHorario.CurrentRow.Cells[0].Value.ToString();
            oTurno.IdPaciente = Convert.ToInt32(lblIdPaciente.Text);
            oTurno.IdHistorial = Convert.ToInt32(lblIdHistorial.Text);
            oTurno.Estado = Turno.Estados.espera;
            if (oTurno.RegistrarTurno(oTurno))
            {
                lblEstado.Text = "Registrado con éxito";
                cargarDataGrid(mtCalendario.SelectionStart);
                vaciarCampos();
            }
            else
            {
                lblEstado.Text = "Hubo un error";
            }
        }
        private void vaciarCampos()
        {
            mtxtNroDocumento.Text = "";
            lblEdad.Text = "";
            lblIdHistorial.Text = "";
            lblIdPaciente.Text = "";
            lblTelefono.Text = "";
            txtCausa.Text = "";
            lblPaciente.Text = "";
            btnRegistrar.Enabled = false;

        }
    }

}
