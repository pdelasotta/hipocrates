﻿namespace CheekiBreeki.CMH.Terminal.Views
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblUsuarioConectado = new System.Windows.Forms.Label();
            this.lblPrivilegio = new System.Windows.Forms.Label();
            this.btnSesion = new System.Windows.Forms.Button();
            this.btnAgendarAtencion = new System.Windows.Forms.Button();
            this.btnCrearPaciente = new System.Windows.Forms.Button();
            this.btnIngresarPaciente = new System.Windows.Forms.Button();
            this.btnAbrirCaja = new System.Windows.Forms.Button();
            this.btnCerrarCaja = new System.Windows.Forms.Button();
            this.btnAnularAtencion = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblUsuarioConectado
            // 
            this.lblUsuarioConectado.AutoSize = true;
            this.lblUsuarioConectado.Location = new System.Drawing.Point(281, 29);
            this.lblUsuarioConectado.Name = "lblUsuarioConectado";
            this.lblUsuarioConectado.Size = new System.Drawing.Size(98, 13);
            this.lblUsuarioConectado.TabIndex = 0;
            this.lblUsuarioConectado.Text = "ConectadoUsuario:";
            // 
            // lblPrivilegio
            // 
            this.lblPrivilegio.AutoSize = true;
            this.lblPrivilegio.Location = new System.Drawing.Point(281, 42);
            this.lblPrivilegio.Name = "lblPrivilegio";
            this.lblPrivilegio.Size = new System.Drawing.Size(52, 13);
            this.lblPrivilegio.TabIndex = 1;
            this.lblPrivilegio.Text = "Privilegio:";
            // 
            // btnSesion
            // 
            this.btnSesion.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSesion.Location = new System.Drawing.Point(499, 24);
            this.btnSesion.Name = "btnSesion";
            this.btnSesion.Size = new System.Drawing.Size(141, 23);
            this.btnSesion.TabIndex = 2;
            this.btnSesion.Text = "Sesión";
            this.btnSesion.UseVisualStyleBackColor = true;
            this.btnSesion.Click += new System.EventHandler(this.btnSesion_Click);
            // 
            // btnAgendarAtencion
            // 
            this.btnAgendarAtencion.Location = new System.Drawing.Point(55, 112);
            this.btnAgendarAtencion.Name = "btnAgendarAtencion";
            this.btnAgendarAtencion.Size = new System.Drawing.Size(128, 23);
            this.btnAgendarAtencion.TabIndex = 3;
            this.btnAgendarAtencion.Text = "Agendar atención";
            this.btnAgendarAtencion.UseVisualStyleBackColor = true;
            this.btnAgendarAtencion.Click += new System.EventHandler(this.btnAgendarAtencion_Click);
            // 
            // btnCrearPaciente
            // 
            this.btnCrearPaciente.Location = new System.Drawing.Point(205, 112);
            this.btnCrearPaciente.Name = "btnCrearPaciente";
            this.btnCrearPaciente.Size = new System.Drawing.Size(128, 23);
            this.btnCrearPaciente.TabIndex = 4;
            this.btnCrearPaciente.Text = "Crear paciente";
            this.btnCrearPaciente.UseVisualStyleBackColor = true;
            this.btnCrearPaciente.Click += new System.EventHandler(this.btnCrearPaciente_Click);
            // 
            // btnIngresarPaciente
            // 
            this.btnIngresarPaciente.Location = new System.Drawing.Point(354, 112);
            this.btnIngresarPaciente.Name = "btnIngresarPaciente";
            this.btnIngresarPaciente.Size = new System.Drawing.Size(128, 23);
            this.btnIngresarPaciente.TabIndex = 5;
            this.btnIngresarPaciente.Text = "Ingresar paciente";
            this.btnIngresarPaciente.UseVisualStyleBackColor = true;
            this.btnIngresarPaciente.Click += new System.EventHandler(this.btnIngresarPaciente_Click);
            // 
            // btnAbrirCaja
            // 
            this.btnAbrirCaja.Location = new System.Drawing.Point(499, 112);
            this.btnAbrirCaja.Name = "btnAbrirCaja";
            this.btnAbrirCaja.Size = new System.Drawing.Size(128, 23);
            this.btnAbrirCaja.TabIndex = 6;
            this.btnAbrirCaja.Text = "Abrir caja";
            this.btnAbrirCaja.UseVisualStyleBackColor = true;
            this.btnAbrirCaja.Click += new System.EventHandler(this.btnAbrirCaja_Click);
            // 
            // btnCerrarCaja
            // 
            this.btnCerrarCaja.Location = new System.Drawing.Point(499, 154);
            this.btnCerrarCaja.Name = "btnCerrarCaja";
            this.btnCerrarCaja.Size = new System.Drawing.Size(128, 23);
            this.btnCerrarCaja.TabIndex = 7;
            this.btnCerrarCaja.Text = "Cerrar caja";
            this.btnCerrarCaja.UseVisualStyleBackColor = true;
            this.btnCerrarCaja.Click += new System.EventHandler(this.btnCerrarCaja_Click);
            // 
            // btnAnularAtencion
            // 
            this.btnAnularAtencion.Location = new System.Drawing.Point(55, 154);
            this.btnAnularAtencion.Name = "btnAnularAtencion";
            this.btnAnularAtencion.Size = new System.Drawing.Size(128, 23);
            this.btnAnularAtencion.TabIndex = 8;
            this.btnAnularAtencion.Text = "Anular atención";
            this.btnAnularAtencion.UseVisualStyleBackColor = true;
            this.btnAnularAtencion.Click += new System.EventHandler(this.btnAnularAtencion_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.btnAnularAtencion);
            this.Controls.Add(this.btnCerrarCaja);
            this.Controls.Add(this.btnAbrirCaja);
            this.Controls.Add(this.btnIngresarPaciente);
            this.Controls.Add(this.btnCrearPaciente);
            this.Controls.Add(this.btnAgendarAtencion);
            this.Controls.Add(this.btnSesion);
            this.Controls.Add(this.lblPrivilegio);
            this.Controls.Add(this.lblUsuarioConectado);
            this.Name = "FrmMain";
            this.Text = "Centro médico Hipócrates";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUsuarioConectado;
        private System.Windows.Forms.Label lblPrivilegio;
        private System.Windows.Forms.Button btnSesion;
        private System.Windows.Forms.Button btnAgendarAtencion;
        private System.Windows.Forms.Button btnCrearPaciente;
        private System.Windows.Forms.Button btnIngresarPaciente;
        private System.Windows.Forms.Button btnAbrirCaja;
        private System.Windows.Forms.Button btnCerrarCaja;
        private System.Windows.Forms.Button btnAnularAtencion;
    }
}