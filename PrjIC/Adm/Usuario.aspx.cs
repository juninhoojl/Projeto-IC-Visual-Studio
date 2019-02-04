﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjetoIC.Classes;

namespace PrjIC.Adm
{
    public partial class Usuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.PopulateGridView();
            }
        }

        private void PopulateGridView()
        {
            Conexao conn = new Conexao();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ProjetoIC"].ConnectionString;

            if (conn.AbrirConexao())
            {
                DataTable tabUsuario = conn.RetornaTabela(@"select * from Usuario");

                if (tabUsuario.Rows.Count > 0)
                { 
                    this.dgvUsuario.DataSource = tabUsuario;
                    this.dgvUsuario.DataBind();
                }
                else
                {
                    tabUsuario.Rows.Add(tabUsuario.NewRow());
                    this.dgvUsuario.DataSource = tabUsuario;
                    this.dgvUsuario.DataBind();
                    this.dgvUsuario.Rows[0].Cells.Clear();
                    this.dgvUsuario.Rows[0].Cells.Add(new TableCell());
                    this.dgvUsuario.Rows[0].Cells[0].ColumnSpan = tabUsuario.Columns.Count;
                    this.dgvUsuario.Rows[0].Cells[0].Text = "Nenhum usuário cadastrado!";
                    this.dgvUsuario.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;

                }

                conn.FechaConexao();
            }
        }

        protected void dgvUsuario_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Conexao conn = new Conexao();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ProjetoIC"].ConnectionString;

                if (conn.AbrirConexao())
                {
                    string cmd = "delete from Usuario where id_Usuario = @id";

                    Dictionary<string, object> sqlParam = new Dictionary<string, object>();
                    sqlParam.Add("@id", Convert.ToInt32(this.dgvUsuario.DataKeys[e.RowIndex].Value.ToString()));
                    conn.ExecutaComando(cmd, sqlParam);
                    this.PopulateGridView();

                    conn.FechaConexao();
                }
            }
            catch (Exception)
            {

            }
        }

        protected void dgvUsuario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("AddNew"))
            {
                try
                {
                    Conexao conn = new Conexao();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["ProjetoIC"].ConnectionString;

                    if (conn.AbrirConexao())
                    {
                        string cmd = "insert into Usuario (ds_Email, ds_Senha, id_Curso) values (@dsEmail, @dsSenha, @idCurso)";

                        Dictionary<string, object> sqlParam = new Dictionary<string, object>();

                        var xx = this.dgvUsuario.FooterRow.FindControl("txtds_EmailFooter") as TextBox;
                        sqlParam.Add("@dsEmail", xx.Text.Trim());
                        sqlParam.Add("@dsSenha", "" );
                        sqlParam.Add("@idCurso", 1);
                        conn.ExecutaComando(cmd, sqlParam);
                        this.PopulateGridView();

                        conn.FechaConexao();
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}