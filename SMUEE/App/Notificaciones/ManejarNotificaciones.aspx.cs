using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMUEE.App.Notificaciones
{
    public partial class ManejarNotificaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                LoadData();
                btnAdd.Visible = true;
                btnDelete.Visible = false;
                btnModify.Visible = false;

                if (Request.QueryString["PkNotification"] != null)
                {
                    var pk = Request.QueryString["PkNotification"];
                    var notification = -1;
                    int.TryParse(pk, out notification);

                    if (notification != -1)
                    {
                        btnAdd.Visible = false;
                        btnDelete.Visible = true;
                        btnModify.Visible = true;
                        btnModify.CommandArgument = notification.ToString();
                        btnDelete.CommandArgument = notification.ToString();

                        using (var smuee = new SMUEEEntities())
                        {
                            var n = smuee.SM_NOTIFICACIONES.FirstOrDefault(x => x.PK_NOTIFICACIONES == notification);
                            var lstUserChk = smuee.SM_NOTIFICACIONES_USUARIO.Where(x => x.FK_NOTIFICACIONES == notification).ToList();

                            if (n != null)
                            {
                                ddlIcon.SelectedValue = n.FK_ICONO.ToString();
                                txtTitle.Value = n.TITULO;
                                txtDescription.Value = n.DE_NOTIFICACIONES;
                                ddlActive.SelectedValue = (n.ACTIVO == true) ? "1" : "0";

                                if (lstUserChk.Count > 0)
                                {
                                    foreach (var user in lstUserChk)
                                    {
                                        var chk = lstUser.Items.FindByValue(user.FK_USUARIO);
                                        if (chk != null)
                                            chk.Selected = true;
                                    }
                                }
                            }
                        }
                    }



                }



            }

        }


        void LoadData()
        {
            using (var smuee = new SMUEEEntities())
            {
                ddlIcon.DataValueField = "PK_ICONO";
                ddlIcon.DataTextField = "DE_ICONO";
                ddlIcon.DataSource = smuee.SM_LKP_ICONO_NOTIFICACIONES.OrderBy(x => x.DE_ICONO).ToList();
                ddlIcon.DataBind();

                lstUser.DataTextField = "Usuario";
                lstUser.DataValueField = "PK_Usuario";
                lstUser.DataSource = smuee.VW_UsersList.OrderBy(x => x.Usuario).ToList();
                lstUser.DataBind();
            }
        }

        protected void btnSelectAll_Click(object sender, EventArgs e)
        {
            if (lstUser.Items.Count > 0)
            {
                foreach (ListItem chk in lstUser.Items)
                {
                    chk.Selected = true;
                }

            }
            lstUser.Focus();
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                if (Page.IsValid)
                {
                    var notification = new SM_NOTIFICACIONES()
                    {
                        TITULO = txtTitle.Value,
                        DE_NOTIFICACIONES = txtDescription.Value,
                        FE_ENVIADO = DateTime.Now,
                        FK_ICONO = int.Parse(ddlIcon.SelectedValue),
                        ACTIVO = (ddlActive.SelectedValue == "1") ? true : false,
                    };



                    using (var smuee = new SMUEEEntities())
                    {
                        notification = smuee.SM_NOTIFICACIONES.Add(notification);
                        smuee.Entry(notification).State = System.Data.Entity.EntityState.Added;

                        if (smuee.SaveChanges() > 0)
                        {
                            if (lstUser.Items.Count > 0)
                            {
                                foreach (ListItem chk in lstUser.Items)
                                {
                                    if (chk.Selected)
                                    {
                                        var ref_notification = new SM_NOTIFICACIONES_USUARIO()
                                        {
                                            FK_NOTIFICACIONES = notification.PK_NOTIFICACIONES,
                                            FK_USUARIO = chk.Value,
                                            VISTO = false

                                        };
                                        smuee.Entry(ref_notification).State = System.Data.Entity.EntityState.Added;


                                    }
                                }
                                smuee.SaveChanges();

                                var mensaje = "Se agregó notificación.";
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notificación agregada", "sweetAlert('Notificación Agregada','" + mensaje + "','success')", true);

                            }
                        }
                        else
                        {
                            var mensaje = "Ha ocurrido un error";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error ", "sweetAlert('Error','" + mensaje + "','error')", true);

                        }
                    }
                }
            }catch
            {
                var mensaje = "Ha ocurrido un error";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error ", "sweetAlert('Error','" + mensaje + "','error')", true);

            }
        }

        protected void btnModify_Click(object sender, EventArgs e)
        {
            try { 
            if (Page.IsValid && btnModify.CommandArgument != null)
            {

                using (var smuee = new SMUEEEntities())
                {
                    var pk = int.Parse(btnModify.CommandArgument.ToString());
                    var notification = smuee.SM_NOTIFICACIONES.FirstOrDefault(x => x.PK_NOTIFICACIONES == pk);
                    notification.TITULO = txtTitle.Value;
                    notification.DE_NOTIFICACIONES = txtDescription.Value;
                    notification.FK_ICONO = int.Parse(ddlIcon.SelectedValue);
                    notification.ACTIVO = (ddlActive.SelectedValue == "1") ? true : false;

                    notification = smuee.SM_NOTIFICACIONES.Add(notification);
                    smuee.Entry(notification).State = System.Data.Entity.EntityState.Modified;


                    var lstOriginal = smuee.SM_NOTIFICACIONES_USUARIO.Where(x => x.FK_NOTIFICACIONES == notification.PK_NOTIFICACIONES).ToList();

                    if (lstUser.Items.Count > 0)
                    {
                        foreach (ListItem item in lstUser.Items)
                        {
                            var ref_not = lstOriginal.FirstOrDefault(x=>x.FK_USUARIO == item.Value);

                            //Selecciono y no se encuentra en la lista original
                            if(item.Selected && ref_not == null)
                            {
                                var ref_notification = new SM_NOTIFICACIONES_USUARIO()
                                {
                                    FK_NOTIFICACIONES = notification.PK_NOTIFICACIONES,
                                    FK_USUARIO = item.Value,
                                    VISTO = false

                                };
                                smuee.Entry(ref_notification).State = System.Data.Entity.EntityState.Added;
                            }
                            //No se selecciono y se encuentra en la lista
                            else if(!item.Selected && ref_not != null)
                            {
                                smuee.Entry(ref_not).State = System.Data.Entity.EntityState.Deleted;

                            }

                        }

                    }

                    smuee.SaveChanges();

                   var mensaje = "Se modificó notificación.";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notificación Actualizada", "sweetAlert('Notificación modificada','" + mensaje + "','success')", true);


                }
            }
            }
            catch
            {
                var mensaje = "Ha ocurrido un error";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error ", "sweetAlert('Error','" + mensaje + "','error')", true);

            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnModify.CommandArgument != null)
                {

                    using (var smuee = new SMUEEEntities())
                    {
                        var pk = int.Parse(btnModify.CommandArgument.ToString());

                        var lstOriginal = smuee.SM_NOTIFICACIONES_USUARIO.Where(x => x.FK_NOTIFICACIONES == pk).ToList();
                        var notification = smuee.SM_NOTIFICACIONES.FirstOrDefault(x => x.PK_NOTIFICACIONES == pk);

                        if (notification != null)
                        {
                            if (lstOriginal.Count > 0)
                            {
                                foreach (var not in lstOriginal)
                                {
                                    smuee.Entry(not).State = System.Data.Entity.EntityState.Deleted;
                                }
                            }
                            smuee.Entry(notification).State = System.Data.Entity.EntityState.Deleted;

                        }

                        if (smuee.SaveChanges() > 0)
                        {
                            var mensaje = "Se eliminó notificación.";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notificación eliminada", "sweetAlert('Notificación modificada','" + mensaje + "','success')", true);
                           
                            Response.Redirect("~/App/Notificaciones/Notificaciones", false);
                        }
                        else
                        {
                            var mensaje = "Se modificó notificación.";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notificación Actualizada", "sweetAlert('Notificación modificada','" + mensaje + "','success')", true);

                        }


                    }
                }
            }
            catch
            {
                var mensaje = "Ha ocurrido un error";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error ", "sweetAlert('Error','" + mensaje + "','error')", true);

            }
        }
    }
}