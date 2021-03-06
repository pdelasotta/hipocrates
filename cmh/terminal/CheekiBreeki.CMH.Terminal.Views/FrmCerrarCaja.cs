﻿using CheekiBreeki.CMH.Terminal.BL;
using CheekiBreeki.CMH.Terminal.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheekiBreeki.CMH.Terminal.Views
{
    public partial class FrmCerrarCaja : Form
    {
        private static AccionesTerminal at = new AccionesTerminal();
        private static List<ENTRADA_FICHA> entradaList;
        //private static PACIENTE paciente;
        FrmLogin login = null;
        bool closeApp;

        public FrmCerrarCaja(FrmLogin fLogin)
        {
            InitializeComponent();
            closeApp = true;
            entradaList = new List<ENTRADA_FICHA>();
            login = fLogin;
            this.StartPosition = FormStartPosition.CenterScreen;

            if (FrmLogin.usuarioLogeado != null)
            {
                lblUsuarioConectado.Text = "Usuario Conectado: " + FrmLogin.usuarioLogeado.NombreUsuario;
                lblPrivilegio.Text = "Privilegio: " + FrmLogin.usuarioLogeado.Privilegio;
                btnSesion.Text = "Cerrar Sesión";
            }
            else
            {
                lblUsuarioConectado.Text = "Debes iniciar sesión";
                lblPrivilegio.Text = "";
                btnSesion.Text = "Iniciar sesión";
            }
            comprobarEstadoCaja();
        }

        #region Funciones comunes
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //                                                                                                                              //
        //   CERRAR SESION                                                                                                              //
        //                                                                                                                              //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnSesion_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea cerrar sesión?", "Cerrar sesión",
                                MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
            {
                closeApp = false;
                login.camposVacios();
                login.Show();
                this.Close();
            }
        }

        private void FrmCerrarCaja_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (closeApp)
                Application.Exit();
        }

        private void btnModificarUser_Click(object sender, EventArgs e)
        {
            InitGB(gbOpcionesUsuario);
        }

        private void InitGB(GroupBox x)
        {
            gbOpcionesUsuario.Hide();
            x.Show();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //                                                                                                                              //
        //   OPCIONES DE CUENTA                                                                                                         //
        //                                                                                                                              //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnCambiarContrasena_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbContrasenaActual.Text.Trim()) && !string.IsNullOrEmpty(tbContrasenaNueva.Text.Trim()))
            {
                if (Util.isObjetoNulo(Login.verificarUsuario(FrmLogin.usuarioLogeado.Personal.EMAIL, tbContrasenaActual.Text)))
                {
                    MessageBox.Show("La contraseña ingresada no es correcta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    PERSONAL personal = Login.verificarUsuario(FrmLogin.usuarioLogeado.Personal.EMAIL, tbContrasenaActual.Text);
                    personal.HASHED_PASS = Util.hashMD5(tbContrasenaNueva.Text.TrimStart().TrimEnd());
                    bool x = at.actualizarPersonal(personal);
                    if (x)
                    {
                        MessageBox.Show("Contraseña actualizada con exito", "Contraseña", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        tbContrasenaActual.Text = "";
                        tbContrasenaNueva.Text = "";
                    }
                    else
                        MessageBox.Show("Error al actualizar contraseña", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (string.IsNullOrEmpty(tbContrasenaActual.Text.Trim()) && string.IsNullOrEmpty(tbContrasenaNueva.Text.Trim()))
            {
                MessageBox.Show("Los dos campos estan vacios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrEmpty(tbContrasenaActual.Text.Trim()))
            {
                MessageBox.Show("El campo de constraseña actual esta vacio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrEmpty(tbContrasenaNueva.Text.Trim()))
            {
                MessageBox.Show("El campo de constraseña nueva esta vacio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCambiarEmail_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbNuevoMail.Text.Trim()) && Util.isEmailValido(tbNuevoMail.Text.Trim()))
            {
                if (MessageBox.Show("¿Seguro que desea cambiar su email?", "Cambiar Email",
                               MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                {
                    PERSONAL personal = FrmLogin.usuarioLogeado.Personal;
                    ;
                    personal.EMAIL = tbNuevoMail.Text.Trim();
                    bool x = at.actualizarPersonal(personal);
                    if (x)
                    {
                        closeApp = false;
                        login.camposVacios();
                        login.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error al modificar el email", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            else if (string.IsNullOrEmpty(tbNuevoMail.Text.Trim()))
            {
                MessageBox.Show("Campo de email vacio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!Util.isEmailValido(tbNuevoMail.Text.Trim()))
            {
                MessageBox.Show("Email invalido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //                                                                                                                              //
        //   BOTONES DEL MENU                                                                                                           //
        //                                                                                                                              //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void agendarHoraToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmAgendarAtencion frmAgendar = new FrmAgendarAtencion(login);
            frmAgendar.Show();
            frmAgendar.Activate();
            this.Hide();
        }

        private void anularHoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAnularAtencion frmAnularAtencion = new FrmAnularAtencion(login);
            frmAnularAtencion.Show();
            frmAnularAtencion.Activate();
            this.Hide();
        }

        private void crearPacienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCrearPaciente frmCrearPaciente = new FrmCrearPaciente(login);
            frmCrearPaciente.Show();
            frmCrearPaciente.Activate();
            this.Hide();
        }

        private void ingresarPacienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmIngresarPaciente frmIngresarPaciente = new FrmIngresarPaciente(login);
            frmIngresarPaciente.Show();
            frmIngresarPaciente.Activate();
            this.Hide();
        }

        private void abrirCajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAbrirCaja frmAbrirCaja = new FrmAbrirCaja(login);
            frmAbrirCaja.Show();
            frmAbrirCaja.Activate();
            this.Hide();
        }

        private void cerrarCjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCerrarCaja frmCerrarCaja = new FrmCerrarCaja(login);
            frmCerrarCaja.Show();
            frmCerrarCaja.Activate();
            this.Hide();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //                                                                                                                              //
        //   DESACTIVAR BOTONES SEGUN ESTADO CAJA                                                                                       //
        //                                                                                                                              //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void comprobarEstadoCaja()
        {
            UsuarioLogeado usuario = FrmLogin.usuarioLogeado;
            if (usuario.Privilegio.ToUpper() == "OPERADOR")
            {
                if (!Util.isObjetoNulo(at.buscarCajaCerrada(usuario.Personal.FUNCIONARIO.FirstOrDefault())))
                {
                    abrirCajaToolStripMenuItem.Enabled = false;
                    ingresarPacienteToolStripMenuItem.Enabled = false;
                    anularHoraToolStripMenuItem.Enabled = false;
                    cerrarCjaToolStripMenuItem.Enabled = false;
                }
                else if (!Util.isObjetoNulo(at.buscarCajaAbierta(usuario.Personal.FUNCIONARIO.FirstOrDefault())))
                {
                    abrirCajaToolStripMenuItem.Enabled = false;
                    ingresarPacienteToolStripMenuItem.Enabled = true;
                    anularHoraToolStripMenuItem.Enabled = true;
                    cerrarCjaToolStripMenuItem.Enabled = true;
                }
                else
                {
                    abrirCajaToolStripMenuItem.Enabled = true;
                    ingresarPacienteToolStripMenuItem.Enabled = false;
                    anularHoraToolStripMenuItem.Enabled = false;
                    cerrarCjaToolStripMenuItem.Enabled = false;
                }
            }
            else
            {
                abrirCajaToolStripMenuItem.Enabled = false;
                cerrarCjaToolStripMenuItem.Enabled = false;
                ingresarPacienteToolStripMenuItem.Enabled = false;
                anularHoraToolStripMenuItem.Enabled = false;
            }
        }
        #endregion

        private void btnCerrarCaja_Click(object sender, EventArgs e)
        {
            string mensajeError = string.Empty;
            try
            {
                FUNCIONARIO funcionario = FrmLogin.usuarioLogeado.Personal.FUNCIONARIO.FirstOrDefault();
                int dinero = int.Parse(txtDinero.Text);
                int cheques = int.Parse(txtCheques.Text);

                lblError.Visible = true;
                lblError.Text = "Cerrando caja...";
                lblError.ForeColor = System.Drawing.Color.Violet;
                btnCerrarCaja.Enabled = false;

                if(!at.cerrarCaja(funcionario, dinero, cheques))
                    mensajeError = "Error al cerrar caja";
            }
            catch (Exception ex)
            {
                mensajeError = "Error al cerrar caja";
            }
            if (mensajeError == string.Empty)
            {
                FrmOperador frmOperador = new FrmOperador(login);
                frmOperador.Show();
                frmOperador.Activate();
                this.Hide();
            }
            else
                MessageBox.Show(mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            btnCerrarCaja.Enabled = true;
        }

        private void txtDinero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
