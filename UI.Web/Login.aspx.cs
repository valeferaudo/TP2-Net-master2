using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Entities;
using Business.Logic;

namespace UI.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtpassword.Text.CompareTo("")!=0 && txtUsuario.Text.CompareTo("")!=0 )
            {
                UsuarioLogic ul = new UsuarioLogic();
                Usuario usuario = ul.Logearse(txtUsuario.Text, txtpassword.Text);
                if (usuario.ID > 0)
                {
                    Page.Response.Write("Login OK");
                    Session["UsuarioSesion"] = usuario;
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    Page.Response.Write("Usuario o clave incorrectos");
                }
            }
            else
            {
                Page.Response.Write("Campos incompletos");
            }
        }
    }
}