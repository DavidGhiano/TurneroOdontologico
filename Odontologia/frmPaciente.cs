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
    public partial class frmPaciente : Form
    {
        public frmPrincipal fPrincipal;
        public frmPaciente(frmPrincipal fPrincipal)
        {
            InitializeComponent();
            this.fPrincipal = fPrincipal;
        }

        private void comprobarCamposLlenos(object sender, KeyEventArgs e)
        {
            if(mtxtNroDocumento.Text != "" && txtNombre.Text != "" && txtApellido.Text !="" && txtDireccion.Text != "" && mtxtTelefono.Text != "(   )   -")
            {
                btnRegistrar.Enabled = true;
            }
            else
            {
                btnRegistrar.Enabled = false;
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            Paciente oPaciente = new Paciente(txtNombre.Text,txtApellido.Text,mtxtNroDocumento.Text,txtDireccion.Text,mtxtTelefono.Text,dtpFechaNac.Value);
            if (oPaciente.ComprobarDni(mtxtNroDocumento.Text))
            {
                if (oPaciente.InsertPaciente(oPaciente))
                {
                    vaciarCampos();
                    lblInfo.Text = "Paciente registrado con éxito";
                }
                else
                {
                    lblInfo.Text = "Hubo un error";
                }
            }
            else
            {
                lblInfo.Text = "Paciente ya registrado";
            }
        }
        private void vaciarCampos()
        {
            mtxtNroDocumento.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtDireccion.Text = "";
            mtxtTelefono.Text = "(   )   -";
            dtpFechaNac.Value = DateTime.Today;

        }
    }
}
