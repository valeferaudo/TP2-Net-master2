using Business.Entities;
using Business.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Util;

namespace UI.Web
{
    public partial class Inscripciones : System.Web.UI.Page
    {
        Usuario alumno;
        Usuario usuario;
        int Modo;
        List<Curso> inscribibles = new List<Curso>();
       
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            Panel2.Visible = false;
            PersonaLogic pl = new PersonaLogic();
            usuario = (Usuario)Session["UsuarioSesion"];
            
            if(pl.GetOne(usuario.IDPersona).TipoPersona == Personas.tipopersona.Alumno)
            {
                btnCalificar.Visible = false;
                btnCalificar.Enabled = false;
                alumno = (Usuario)Session["UsuarioSesion"];
            }
            if(pl.GetOne(usuario.IDPersona).TipoPersona == Personas.tipopersona.Admin || pl.GetOne(usuario.IDPersona).TipoPersona == Personas.tipopersona.Docente)
            {
                if ((Usuario)Session["AlumnoInscSel"] == null)
                {
                    //En caso de que el administrador o docente acceda a inscripciones desde master page o url
                    Response.Redirect("~/Default.aspx");
                }
                alumno = (Usuario)Session["AlumnoInscSel"];
            }
            this.Label1.Text = alumno.Nombre + " " + alumno.Apellido;
            this.ListarInscripciones();
            if (!IsPostBack)
            {
                this.ListarMateriasDisp();
            }
            else
            {
                this.ListarMateriasDisp();
            }

        }
        private AlumnoInscripcion Entity
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

        protected void gridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedID = (int)this.GridViewInscripciones.SelectedValue;
            
            
        }
        private void ListarInscripciones()
        {
            PersonaLogic pl = new PersonaLogic();
            InscripcionLogic il = new InscripcionLogic();
            GridViewInscripciones.DataSource = il.GetInscAlumno(pl.GetOne(alumno.IDPersona));
            GridViewInscripciones.DataBind();
        }
        private void ListarMateriasDisp()
        {
            CursoLogic cl = new CursoLogic();
            List<Curso> cursos = cl.GetDispAlumno(alumno.ID);
            DropDownList1.Items.Clear();
            inscribibles = new List<Curso>();
            foreach (Curso value in cursos)
            {

                if (cl.YaAprobado(alumno.ID, value) == false)
                {
                    DropDownList1.Items.Add(value.Descripcion);
                    
                    inscribibles.Add(value);
                }

            }

        }

        protected void btnInscribir_Click(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedIndex > -1)
            {
                this.Inscribir();
                Response.Redirect("~/Inscripciones.aspx");
            }
        }
        public void Inscribir()
        {
            int nrocurso;
            CursoLogic cl = new CursoLogic();

            nrocurso = inscribibles[DropDownList1.SelectedIndex].ID;
            AlumnoInscripcion alinsc = new AlumnoInscripcion();
            alinsc.IDAlumno = alumno.ID;
            alinsc.IDCurso = nrocurso;
            alinsc.Condicion = "Inscripto";
            alinsc.State = BusinessEntity.States.New;
            InscripcionLogic il = new InscripcionLogic();
            il.Save(alinsc);
        }

        protected void btnGuardarCalificacion_Click(object sender, EventArgs e)
        {
            if (ValidacionIngresoDatos.EsNumero(TextBox1.Text))
            {
                int nota = Convert.ToInt32(TextBox1.Text);
                if(nota>=0 && nota <= 10)
                {
                    InscripcionLogic il = new InscripcionLogic();
                    AlumnoInscripcion ai = new AlumnoInscripcion();
                    ai = il.GetOne(SelectedID);
                    ai.Nota = Convert.ToInt32(TextBox1.Text);
                    ai.Condicion = DropDownList2.SelectedItem.ToString();
                    ai.State = BusinessEntity.States.Modified;
                    il.Save(ai);
                    Response.Redirect("~/Inscripciones.aspx");
                }
            }
        }

        protected void btnNuevaInscripcion_Click(object sender, EventArgs e)
        {
            Panel1.Visible = true;
            Panel2.Visible = false;
        }

        protected void btnCalificar_Click(object sender, EventArgs e)
        {
            if (SelectedID > 0)
            {
                Panel2.Visible = true;
                Panel1.Visible = false;
            }
        }
        
        
    }
}