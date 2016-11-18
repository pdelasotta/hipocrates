﻿namespace CheekiBreeki.CMH.Terminal.Views
{
    partial class FrmAnularAtencion
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDv = new System.Windows.Forms.TextBox();
            this.txtRut = new System.Windows.Forms.TextBox();
            this.lblError = new System.Windows.Forms.Label();
            this.btnAnular = new System.Windows.Forms.Button();
            this.lstAtenciones = new System.Windows.Forms.ListBox();
            this.brnBuscarAtenciones = new System.Windows.Forms.Button();
            this.lblRut = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblAseguradora = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblDescuento = new System.Windows.Forms.Label();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblRutInfo = new System.Windows.Forms.Label();
            this.lblSexo = new System.Windows.Forms.Label();
            this.lblEdad = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRazon = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtRazon);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.txtDv);
            this.groupBox1.Controls.Add(this.txtRut);
            this.groupBox1.Controls.Add(this.lblError);
            this.groupBox1.Controls.Add(this.btnAnular);
            this.groupBox1.Controls.Add(this.lstAtenciones);
            this.groupBox1.Controls.Add(this.brnBuscarAtenciones);
            this.groupBox1.Controls.Add(this.lblRut);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(760, 521);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Anular atención";
            // 
            // txtDv
            // 
            this.txtDv.Location = new System.Drawing.Point(350, 50);
            this.txtDv.MaxLength = 1;
            this.txtDv.Name = "txtDv";
            this.txtDv.Size = new System.Drawing.Size(15, 20);
            this.txtDv.TabIndex = 19;
            this.txtDv.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDv_KeyPress);
            // 
            // txtRut
            // 
            this.txtRut.Location = new System.Drawing.Point(244, 50);
            this.txtRut.MaxLength = 8;
            this.txtRut.Name = "txtRut";
            this.txtRut.Size = new System.Drawing.Size(99, 20);
            this.txtRut.TabIndex = 18;
            this.txtRut.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRut_KeyPress);
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Location = new System.Drawing.Point(260, 84);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(39, 13);
            this.lblError.TabIndex = 17;
            this.lblError.Text = "lblError";
            this.lblError.Visible = false;
            // 
            // btnAnular
            // 
            this.btnAnular.Location = new System.Drawing.Point(457, 450);
            this.btnAnular.Name = "btnAnular";
            this.btnAnular.Size = new System.Drawing.Size(131, 34);
            this.btnAnular.TabIndex = 16;
            this.btnAnular.Text = "Anular atención";
            this.btnAnular.UseVisualStyleBackColor = true;
            this.btnAnular.Click += new System.EventHandler(this.btnAnular_Click);
            // 
            // lstAtenciones
            // 
            this.lstAtenciones.FormattingEnabled = true;
            this.lstAtenciones.Location = new System.Drawing.Point(117, 110);
            this.lstAtenciones.Name = "lstAtenciones";
            this.lstAtenciones.Size = new System.Drawing.Size(496, 134);
            this.lstAtenciones.TabIndex = 15;
            this.lstAtenciones.SelectedIndexChanged += new System.EventHandler(this.lstAtenciones_SelectedIndexChanged);
            // 
            // brnBuscarAtenciones
            // 
            this.brnBuscarAtenciones.Location = new System.Drawing.Point(402, 47);
            this.brnBuscarAtenciones.Name = "brnBuscarAtenciones";
            this.brnBuscarAtenciones.Size = new System.Drawing.Size(167, 23);
            this.brnBuscarAtenciones.TabIndex = 14;
            this.brnBuscarAtenciones.Text = "Buscar atenciones agendadas";
            this.brnBuscarAtenciones.UseVisualStyleBackColor = true;
            this.brnBuscarAtenciones.Click += new System.EventHandler(this.brnBuscarAtenciones_Click);
            // 
            // lblRut
            // 
            this.lblRut.AutoSize = true;
            this.lblRut.Location = new System.Drawing.Point(137, 57);
            this.lblRut.Name = "lblRut";
            this.lblRut.Size = new System.Drawing.Size(75, 13);
            this.lblRut.TabIndex = 11;
            this.lblRut.Text = "RUT Paciente";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblAseguradora);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.lblTotal);
            this.groupBox3.Controls.Add(this.lblDescuento);
            this.groupBox3.Controls.Add(this.lblSubtotal);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(380, 260);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(233, 176);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Devolución";
            // 
            // lblAseguradora
            // 
            this.lblAseguradora.AutoSize = true;
            this.lblAseguradora.Location = new System.Drawing.Point(131, 136);
            this.lblAseguradora.Name = "lblAseguradora";
            this.lblAseguradora.Size = new System.Drawing.Size(77, 13);
            this.lblAseguradora.TabIndex = 28;
            this.lblAseguradora.Text = "lblAseguradora";
            this.lblAseguradora.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(29, 136);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 13);
            this.label9.TabIndex = 27;
            this.label9.Text = "Aseguradora:";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(131, 102);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(41, 13);
            this.lblTotal.TabIndex = 26;
            this.lblTotal.Text = "lblTotal";
            this.lblTotal.Visible = false;
            // 
            // lblDescuento
            // 
            this.lblDescuento.AutoSize = true;
            this.lblDescuento.Location = new System.Drawing.Point(131, 70);
            this.lblDescuento.Name = "lblDescuento";
            this.lblDescuento.Size = new System.Drawing.Size(69, 13);
            this.lblDescuento.TabIndex = 25;
            this.lblDescuento.Text = "lblDescuento";
            this.lblDescuento.Visible = false;
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Location = new System.Drawing.Point(131, 37);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(56, 13);
            this.lblSubtotal.TabIndex = 10;
            this.lblSubtotal.Text = "lblSubtotal";
            this.lblSubtotal.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(29, 102);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "Total: ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(29, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Desc. por bono:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Subtotal: ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblRutInfo);
            this.groupBox2.Controls.Add(this.lblSexo);
            this.groupBox2.Controls.Add(this.lblEdad);
            this.groupBox2.Controls.Add(this.lblNombre);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(117, 260);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(248, 176);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Información paciente";
            // 
            // lblRutInfo
            // 
            this.lblRutInfo.AutoSize = true;
            this.lblRutInfo.Location = new System.Drawing.Point(127, 136);
            this.lblRutInfo.Name = "lblRutInfo";
            this.lblRutInfo.Size = new System.Drawing.Size(52, 13);
            this.lblRutInfo.TabIndex = 8;
            this.lblRutInfo.Text = "lblRutInfo";
            this.lblRutInfo.Visible = false;
            // 
            // lblSexo
            // 
            this.lblSexo.AutoSize = true;
            this.lblSexo.Location = new System.Drawing.Point(127, 102);
            this.lblSexo.Name = "lblSexo";
            this.lblSexo.Size = new System.Drawing.Size(41, 13);
            this.lblSexo.TabIndex = 7;
            this.lblSexo.Text = "lblSexo";
            this.lblSexo.Visible = false;
            // 
            // lblEdad
            // 
            this.lblEdad.AutoSize = true;
            this.lblEdad.Location = new System.Drawing.Point(127, 70);
            this.lblEdad.Name = "lblEdad";
            this.lblEdad.Size = new System.Drawing.Size(42, 13);
            this.lblEdad.TabIndex = 6;
            this.lblEdad.Text = "lblEdad";
            this.lblEdad.Visible = false;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(127, 37);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(54, 13);
            this.lblNombre.TabIndex = 5;
            this.lblNombre.Text = "lblNombre";
            this.lblNombre.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "RUT: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Sexo:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Fecha de nacimiento:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre:";
            // 
            // txtRazon
            // 
            this.txtRazon.Location = new System.Drawing.Point(204, 458);
            this.txtRazon.Name = "txtRazon";
            this.txtRazon.Size = new System.Drawing.Size(229, 20);
            this.txtRazon.TabIndex = 25;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(137, 465);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Razón: ";
            // 
            // FrmAnularAtencion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmAnularAtencion";
            this.Text = "Anular atención";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmIngresarPaciente_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button brnBuscarAtenciones;
        private System.Windows.Forms.Label lblRut;
        private System.Windows.Forms.Button btnAnular;
        private System.Windows.Forms.ListBox lstAtenciones;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.TextBox txtDv;
        private System.Windows.Forms.TextBox txtRut;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblAseguradora;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblDescuento;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblRutInfo;
        private System.Windows.Forms.Label lblSexo;
        private System.Windows.Forms.Label lblEdad;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRazon;
        private System.Windows.Forms.Label label5;
    }
}