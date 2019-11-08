using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business.Entities;
using Business.Logic;
using Util;

namespace UI.Desktop
{
    public partial class ComisionesDesktop : UI.Desktop.Abm
    {
        public ComisionesDesktop()
        {
            InitializeComponent();
        }
        public void Notificar(string titulo, string mensaje, MessageBoxButtons botones, MessageBoxIcon icono)
        {
            MessageBox.Show(mensaje, titulo, botones, icono);
        }
        public void Notificar(string mensaje, MessageBoxButtons botones, MessageBoxIcon icono)
        {
            this.Notificar(this.Text, mensaje, botones, icono);
        }
        private Comision _ComisionActual;

        public Comision ComisionActual
        {
            get { return _ComisionActual; }
            set { _ComisionActual = value; }
        }
        public ComisionesDesktop(ModoForm modo) : this()
        {
            this.Modo = modo;
            this.SetCBComision();
        }
        public ComisionesDesktop(int ID, ModoForm modo) : this()
        {          
            this.Modo = modo;
            ComisionLogic cl = new ComisionLogic();
            ComisionActual = cl.GetOne(ID);
            MapearDeDatos();
            this.SetCBComision();
        }
        public override void MapearDeDatos()

        {
            this.txtIDComision.Text = this.ComisionActual.ID.ToString();
            this.txtDescripcion.Text = this.ComisionActual.Descripcion;
            this.txtAnioEspe.Text = this.ComisionActual.AnioEspecialidad.ToString();
            //this.cbIDPlan.SelectedItem = this.ComisionActual.IDPlan.ToString();


            switch (Modo)
            {
                case ModoForm.Alta:
                    {
                        this.btnAceptar.Text = "Guardar";
                    }
                    break;
                case ModoForm.Baja:
                    {
                        this.btnAceptar.Text = "Eliminar";
                        txtAnioEspe.ReadOnly = true;
                        txtDescripcion.ReadOnly = true;
                        cbIDPlan.Enabled = false;
                    }
                    break;
                case ModoForm.Modificacion:
                    {
                        this.btnAceptar.Text = "Guardar";
                    }
                    break;
                case ModoForm.Consulta:
                    {
                        this.btnAceptar.Text = "Aceptar";
                    }
                    break;
                default:
                    break;
            }
        }
        public override void MapearADatos()
        {
            switch (Modo)
            {
                case ModoForm.Alta:
                    {
                        Comision comi = new Comision();
                        this.ComisionActual = comi;
                        this.ComisionActual.Descripcion = this.txtDescripcion.Text;
                        this.ComisionActual.State = Business.Entities.Plan.States.New;
                        this.ComisionActual.AnioEspecialidad = Int32.Parse(this.txtAnioEspe.Text);
                        
                        this.ComisionActual.IDPlan = Convert.ToInt32(cbIDPlan.SelectedValue);
                    }
                    break;
                case ModoForm.Modificacion:
                    {

                        this.ComisionActual.Descripcion = this.txtDescripcion.Text;
                        this.ComisionActual.AnioEspecialidad = Int32.Parse(this.txtAnioEspe.Text);
                        this.ComisionActual.State = Business.Entities.Plan.States.Modified;
                        this.ComisionActual.IDPlan = Convert.ToInt32(cbIDPlan.SelectedValue);
                    }
                    break;
                case ModoForm.Baja:
                    {
                        this.ComisionActual.State = Business.Entities.Plan.States.Deleted;

                    }
                    break;
                case ModoForm.Consulta:
                    {
                        this.ComisionActual.State = Business.Entities.Plan.States.Unmodified;
                    }
                    break;
                default:
                    break;
            }
        }
        public override void GuardarCambios()
        {

            this.MapearADatos();
            ComisionLogic cl = new ComisionLogic();
            try
            {
                cl.Save(ComisionActual);
            }
            catch(Exception e)
            {
                MessageBox.Show("No se puede eliminar la Comision porque hay Cursos relacionados a esta");
            }
        }


        public override bool Validar()
        {
            if (!(Modo == ModoForm.Baja))
            {
                if ((this.txtDescripcion.Text != "" && this.txtAnioEspe.Text != "" && cbIDPlan.SelectedIndex != -1))
                {
                    if (ValidacionIngresoDatos.EsNumero(txtAnioEspe.Text))
                    {
                        return true;
                    }
                    else
                    {
                        Notificar("Error en llenado de campos", "Ingrese un año de especialidad correcto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    Notificar("Error en llenado de campos", "Alguno de los campos se encuentra vacio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                
            }
            else
            {
                
                
                    if ((this.txtDescripcion.Text != "" && this.txtAnioEspe.Text != "" ))
                    {
                        if (ValidacionIngresoDatos.EsNumero(txtAnioEspe.Text))
                        {
                            return true;
                        }
                        else
                        {
                            Notificar("Error en llenado de campos", "Ingrese un año de especialidad correcto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                    else
                    {
                        Notificar("Error en llenado de campos", "Alguno de los campos se encuentra vacio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                
            }
        }
    
        public void SetCBComision()
        {
            PlanLogic pl = new PlanLogic();

            List<Plan> planes = new List<Plan>();

            if (ComisionActual != null)
            {
                planes = pl.TraerPorEspecialidad(pl.GetOne(ComisionActual.IDPlan).IDEspecialidad);
              foreach (Plan plan in planes)
              {
                  ComboboxItem item = new ComboboxItem();
                  item.Text = plan.Descripcion;
                  item.Value = plan.ID;

                  cbIDPlan.Items.Add(item);
              }
            string plstr = pl.GetOne(ComisionActual.IDPlan).Descripcion;
            cbIDPlan.SelectedIndex = cbIDPlan.FindStringExact(plstr);
                      
            }
            else
            {
                cbIDPlan.DataSource = pl.GetAll();
                cbIDPlan.DisplayMember = "Descripcion";
                cbIDPlan.ValueMember = "ID" ;
                cbIDPlan.SelectedIndex = -1;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                this.GuardarCambios();
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
