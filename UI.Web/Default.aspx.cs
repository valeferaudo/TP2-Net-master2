using Business.Entities;
using Business.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.Web
{
    public partial class Default : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            
            PersonaLogic pl = new PersonaLogic();
            if (Session["UsuarioSesion"] == null)
            {
                //Redirigir a login
                Response.Redirect("~/Login.aspx");
            }
            Usuario usuario = (Usuario)Session["UsuarioSesion"];
            if (pl.GetOne(usuario.IDPersona).TipoPersona == Personas.tipopersona.Admin)
            {
                LinkButton lb = this.Master.FindControl("mis-insc-menu") as LinkButton;
                if (lb != null)
                    lb.Visible = false;
            }
            
        }
    }
}