using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Business.Entities;
using Business.Logic;

namespace UI.Web
{
    public partial class Cursos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["AlumnoInscSel"] = null;
            }

            PersonaLogic pl = new PersonaLogic();
            Usuario usuario = new Usuario();
            if (Session["UsuarioSesion"] != null)
            {
                usuario = (Usuario)Session["UsuarioSesion"];
            }
            if (!(pl.GetOne(usuario.IDPersona).TipoPersona == Personas.tipopersona.Admin) && !(pl.GetOne(usuario.IDPersona).TipoPersona == Personas.tipopersona.Docente))
            {
                Response.Redirect("~/Default.aspx");
            }
            this.LoadGrid();
            if (!IsPostBack)
            {
                
             //this.LlenarComboMateria();
             // this.LlenarComboComision();
               
            }
            if (IsPostBack)
            {

            }

        }
        private void LoadGrid()
        {
            CursoLogic cl = new CursoLogic();
            MateriaLogic ml = new MateriaLogic();
            ComisionLogic coml = new ComisionLogic();
            DataTable dt = new DataTable();
            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add("ID", typeof(string));
                dt.Columns.Add("Materia", typeof(string));
                dt.Columns.Add("Comision", typeof(string));
                dt.Columns.Add("AnioCalendario", typeof(string));
                dt.Columns.Add("Cupo", typeof(string));
                               
            }
            List<Curso> cursos = cl.GetAll();
            foreach (Curso curso in cursos)
            {
                DataRow dr = dt.NewRow();
                dr[0] = Convert.ToString(curso.ID);
                dr[1] = ml.GetOne(curso.IDMateria).Descripcion;
                dr[2] = coml.GetOne(curso.IDComision).Descripcion;
                dr[3] = curso.AnioCalendario;
                dr[4] = curso.Cupo;
                dt.Rows.Add(dr);
            }

            this.GridViewCursos.DataSource = dt;
            this.GridViewCursos.DataBind();
        }
        private CursoLogic _Curso;

        public CursoLogic Curso
        {
            get
            {
                if (_Curso == null)
                {
                    _Curso = new CursoLogic();
                }
                return _Curso;
            }
            set { _Curso = value; }
        }

        public enum formModes
        {
            Alta,
            Baja,
            Modificacion
        }
        public formModes formMode
        {
            get { return (formModes)this.ViewState["formMode"]; }
            set { this.ViewState["formMode"] = value; }
        }
        private Curso Entity
        {
            get;
            set;
        }
        private int SelectedID
        {
            get
            {
                if (this.ViewState["SelectedID"] != null)
                {
                    return (int)this.ViewState["SelectedID"];
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                this.ViewState["SelectedID"] = value;
            }
        }

        private bool IsEntitySelected
        {
            get
            {
                return (this.SelectedID != 0);
            }
        }

        protected void GridViewCursos_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedID = (int)Convert.ToInt32(this.GridViewCursos.SelectedRow.Cells[0].Text);
            MostrarBotones();
            this.formPanel.Visible = false;
            this.ClearForm();
        }
        private void LoadForm(int ID)
        {
            MateriaLogic ml = new MateriaLogic();
            ComisionLogic coml = new ComisionLogic();
            this.Entity = this.Curso.GetOne(ID);
            this.anioCalendarioTextBox.Text = (this.Entity.AnioCalendario).ToString();
            this.cupoTextBox.Text = (this.Entity.Cupo).ToString();
            this.LlenarComboMateria();
            this.LlenarComboComision();
            
            List<Materia> materias = ml.GetAll();
            Materia materia = ml.GetOne(Entity.IDMateria);
            foreach (Materia mat in materias)
            {
                if (mat.ID == materia.ID)
                {
                    this.ddlMateria.SelectedValue = (this.Entity.IDMateria).ToString();
                }
            }
            List<Comision> comisiones = coml.GetAll();
            Comision comision = coml.GetOne(Entity.IDComision);
            foreach (Comision comi in comisiones)
            {
                if (comi.ID == comision.ID)
                {
                    this.ddlComision.SelectedValue = (this.Entity.IDComision).ToString();
                }
            }

        }

        protected void editarLinkButton_Click(object sender, EventArgs e)
        {
            this.LlenarComboMateria();
            this.LlenarComboComision();
            if (this.IsEntitySelected)
            {
                OcultarBotones();
                this.formPanel.Visible = true;
                this.formMode = formModes.Modificacion;
                this.EnableForm(true);
                this.LoadForm(this.SelectedID);
            }
        }
        private void LoadEntity(Curso cur)
        {
            cur.IDMateria = Int32.Parse(this.ddlMateria.SelectedValue);
            cur.IDComision = Int32.Parse(this.ddlComision.SelectedValue);
            cur.AnioCalendario = Int32.Parse(this.anioCalendarioTextBox.Text);
            cur.Cupo = Int32.Parse(this.cupoTextBox.Text);
        }
        private void SaveEntity(Curso curso)
        {
            try
            {
                this.Curso.Save(curso);
            }
            catch (Exception)
            {
                Response.Write("<script>alert('No se puede Modificar o Eliminar, otros registros referencian a este')</script>");
            }
        }

        protected void aceptarLinkButton_Click(object sender, EventArgs e)
        {
            switch (this.formMode)
            {
                case formModes.Alta:
                    {
                        this.Entity = new Curso();
                        this.Entity.State = BusinessEntity.States.New;
                        this.LoadEntity(this.Entity);
                        this.SaveEntity(this.Entity);
                        this.LoadGrid();


                    }
                    break;
                case formModes.Baja:
                    {
                        //this.Entity.State = BusinessEntity.States.Deleted;
                        //tira error
                        //Agarrar la excepcion y mostrar alerta
                        try
                        {
                            this.DeleteEntity(this.SelectedID);
                        }
                        catch (Exception)
                        {
                            Response.Write("<script>alert('No se puede eliminar, otros registros referencian a este')</script>");
                        }
                        this.LoadGrid();


                    }
                    break;
                case formModes.Modificacion:
                    {
                        this.Entity = new Curso();
                        this.Entity.ID = this.SelectedID;
                        this.Entity.State = BusinessEntity.States.Modified;
                        this.LoadEntity(this.Entity);
                        this.SaveEntity(this.Entity);
                        this.LoadGrid();
                    }
                    break;
                default:
                    break;
            }
            this.LoadGrid();
            MostrarBotones();
            this.formPanel.Visible = false;
        }
        private void EnableForm(bool enable)
        {
            this.anioCalendarioLabel.Visible = enable;
            this.cupoLabel.Visible = enable;
            this.idComisionLabel.Visible = enable;
            this.idMateriaLabel.Visible = enable;
            this.anioCalendarioTextBox.Enabled = enable;
            this.cupoTextBox.Enabled = enable;
            this.ddlComision.Enabled = enable;
            this.ddlMateria.Enabled = enable;

        }

        protected void eliminarLinkButton_Click(object sender, EventArgs e)
        {
            if (this.IsEntitySelected)
            {
                OcultarBotones();
                this.formMode = formModes.Baja;
                this.EnableForm(false);
                try
                {
                    this.DeleteEntity(this.SelectedID);
                }
                catch (Exception)
                {
                    Response.Write("<script>alert('No se puede eliminar, otros registros referencian a este')</script>");
                }
                this.LoadGrid();
            }
        }
        private void DeleteEntity(int ID)
        {
            this.Curso.Delete(ID);
        }

        protected void nuevoLinkButton_Click(object sender, EventArgs e)
        {
            this.LlenarComboMateria();
            this.LlenarComboComision();
            OcultarBotones();
            this.formPanel.Visible = true;
            this.formMode = formModes.Alta;
            this.ClearForm();
            this.EnableForm(true);
            this.GridViewCursos.SelectedIndex = -1;
        }
        private void ClearForm()
        {
            this.anioCalendarioTextBox.Text = string.Empty;
            this.cupoTextBox.Text = string.Empty;
        }

        protected void cancelarLinkButton_Click(object sender, EventArgs e)
        {
            MostrarBotones();
            this.formPanel.Visible = false;
            this.ClearForm();
        }
        private void OcultarBotones()
        {
            nuevoLinkButton.Visible = false;
            eliminarLinkButton.Visible = false;
            editarLinkButton.Visible = false;
        }
        private void MostrarBotones()
        {
            nuevoLinkButton.Visible = true;
            eliminarLinkButton.Visible = true;
            editarLinkButton.Visible = true;
        }
        public void LlenarComboMateria()
        {
            ddlMateria.Items.Clear(); 

            MateriaLogic ml = new MateriaLogic();
            if (this.Entity != null)
            {
                /*ist<Materia> materias = ml.TraerPorPlan((ml.GetOne(Entity.IDMateria)).IDPlan);
                 foreach (Materia mat in materias)
                 {
                     ListItem item = new ListItem();
                     item.Text = mat.Descripcion;
                     item.Value = Convert.ToString(mat.ID);

                     ddlMateria.Items.Add(item);
                 }*/
                ddlMateria.DataSource = ml.TraerPorPlan((ml.GetOne(Entity.IDMateria)).IDPlan);
                ddlMateria.DataTextField = "Descripcion";
                ddlMateria.DataValueField = "ID";
                ddlMateria.DataBind();
            }
            else
            {
                /*List<Materia> materias = ml.GetAll();
                  foreach (Materia mat in materias)
                  {
                      ListItem item = new ListItem();
                      item.Text = mat.Descripcion;
                      item.Value = Convert.ToString(mat.ID);

                      ddlMateria.Items.Add(item);
                  } */
                ddlMateria.DataSource = ml.GetAll();
                ddlMateria.DataTextField = "Descripcion";
                ddlMateria.DataValueField = "ID";
                ddlMateria.DataBind();
            }
            
            
        }
        public void LlenarComboComision()
        {
            ddlComision.Items.Clear();
          if(this.Entity != null)
            {
                ComisionLogic coml = new ComisionLogic();
                MateriaLogic ml = new MateriaLogic();
                List<Comision> comisiones = coml.TraerPorPlan(ml.GetOne(Entity.IDMateria).IDPlan);
                foreach (Comision comi in comisiones)
                {
                    ListItem item = new ListItem();
                    item.Text = comi.Descripcion;
                    item.Value = Convert.ToString(comi.ID);

                    ddlComision.Items.Add(item);
                }
            }
            else
            {
                ComisionLogic coml = new ComisionLogic();
                MateriaLogic ml = new MateriaLogic();
                List<Comision> comisiones = coml.GetAll();
                foreach (Comision comi in comisiones)
                {
                    ListItem item = new ListItem();
                    item.Text = comi.Descripcion;
                    item.Value = Convert.ToString(comi.ID);

                    ddlComision.Items.Add(item);
                }
            }
                          
        }

        
    }

 }
