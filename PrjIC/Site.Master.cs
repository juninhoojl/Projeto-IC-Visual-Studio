﻿using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace WebApplication1
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                this.btnEntrar.Visible = false;
                this.btnSair.Visible = true;
            }
            else
            {
                this.btnEntrar.Visible = true;
                this.btnSair.Visible = false;
            }
        }
        protected void btnEntrar_Click(object sender, EventArgs e)
        {
           this.Response.Redirect("~/Adm/Opcoes.aspx");
        }
        protected void btnSair_Click(object sender, EventArgs e)
        {
            //this.Session["nmCliente"] = null;
            //this.Session["cdIdentificacao"] = null;
            //this.Session["cnpjEmpresa"] = null;

            FormsAuthentication.SignOut();
            this.Response.Redirect("~/Apresentacao.aspx");
        }

        protected void ContentPlaceHolder_Load(object sender, EventArgs e)
        {
            if (sender is System.Web.UI.WebControls.ContentPlaceHolder ctrl)
            {
                if (ctrl.Page is Page pagina)
                {
                    if (pagina.AppRelativeVirtualPath.Contains("Login"))
                    {
                        this.btnEntrar.Visible = false;
                    }
                    else
                    {
                    }
                }
            }
        }
    }
}