/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package cl.cheekibreeki.cmh.lib.dal.entities;

import java.io.Serializable;
import java.util.Collection;
import java.util.Date;
import javax.persistence.Basic;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.OneToMany;
import javax.persistence.Table;
import javax.persistence.Temporal;
import javax.persistence.TemporalType;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlTransient;

/**
 *
 * @author dev
 */
@Entity
@Table(name = "PACIENTE")
@XmlRootElement
@NamedQueries({
    @NamedQuery(name = "Paciente.findAll", query = "SELECT p FROM Paciente p"),
    @NamedQuery(name = "Paciente.findByIdPaciente", query = "SELECT p FROM Paciente p WHERE p.idPaciente = :idPaciente"),
    @NamedQuery(name = "Paciente.findByNombresPaciente", query = "SELECT p FROM Paciente p WHERE p.nombresPaciente = :nombresPaciente"),
    @NamedQuery(name = "Paciente.findByApellidosPaciente", query = "SELECT p FROM Paciente p WHERE p.apellidosPaciente = :apellidosPaciente"),
    @NamedQuery(name = "Paciente.findByRut", query = "SELECT p FROM Paciente p WHERE p.rut = :rut"),
    @NamedQuery(name = "Paciente.findByDigitoVerificador", query = "SELECT p FROM Paciente p WHERE p.digitoVerificador = :digitoVerificador"),
    @NamedQuery(name = "Paciente.findByHashedPass", query = "SELECT p FROM Paciente p WHERE p.hashedPass = :hashedPass"),
    @NamedQuery(name = "Paciente.findByEmailPaciente", query = "SELECT p FROM Paciente p WHERE p.emailPaciente = :emailPaciente"),
    @NamedQuery(name = "Paciente.findBySexo", query = "SELECT p FROM Paciente p WHERE p.sexo = :sexo"),
    @NamedQuery(name = "Paciente.findByFecNac", query = "SELECT p FROM Paciente p WHERE p.fecNac = :fecNac"),
    @NamedQuery(name = "Paciente.findByActivo", query = "SELECT p FROM Paciente p WHERE p.activo = :activo")})
public class Paciente implements Serializable {

    private static final long serialVersionUID = 1L;
    @Id
    @Basic(optional = false)
    @Column(name = "ID_PACIENTE")
    private Integer idPaciente;
    @Column(name = "NOMBRES_PACIENTE")
    private String nombresPaciente;
    @Column(name = "APELLIDOS_PACIENTE")
    private String apellidosPaciente;
    @Basic(optional = false)
    @Column(name = "RUT")
    private int rut;
    @Basic(optional = false)
    @Column(name = "DIGITO_VERIFICADOR")
    private Character digitoVerificador;
    @Column(name = "HASHED_PASS")
    private String hashedPass;
    @Column(name = "EMAIL_PACIENTE")
    private String emailPaciente;
    @Column(name = "SEXO")
    private Character sexo;
    @Column(name = "FEC_NAC")
    @Temporal(TemporalType.TIMESTAMP)
    private Date fecNac;
    @Column(name = "ACTIVO")
    private Short activo;
    @OneToMany(mappedBy = "idPaciente")
    private Collection<EntradaFicha> entradaFichaCollection;
    @OneToMany(mappedBy = "idPaciente")
    private Collection<AtencionAgen> atencionAgenCollection;

    public Paciente() {
    }

    public Paciente(Integer idPaciente) {
        this.idPaciente = idPaciente;
    }

    public Paciente(Integer idPaciente, int rut, Character digitoVerificador) {
        this.idPaciente = idPaciente;
        this.rut = rut;
        this.digitoVerificador = digitoVerificador;
    }

    public Integer getIdPaciente() {
        return idPaciente;
    }

    public void setIdPaciente(Integer idPaciente) {
        this.idPaciente = idPaciente;
    }

    public String getNombresPaciente() {
        return nombresPaciente;
    }

    public void setNombresPaciente(String nombresPaciente) {
        this.nombresPaciente = nombresPaciente;
    }

    public String getApellidosPaciente() {
        return apellidosPaciente;
    }

    public void setApellidosPaciente(String apellidosPaciente) {
        this.apellidosPaciente = apellidosPaciente;
    }

    public int getRut() {
        return rut;
    }

    public void setRut(int rut) {
        this.rut = rut;
    }

    public Character getDigitoVerificador() {
        return digitoVerificador;
    }

    public void setDigitoVerificador(Character digitoVerificador) {
        this.digitoVerificador = digitoVerificador;
    }

    public String getHashedPass() {
        return hashedPass;
    }

    public void setHashedPass(String hashedPass) {
        this.hashedPass = hashedPass;
    }

    public String getEmailPaciente() {
        return emailPaciente;
    }

    public void setEmailPaciente(String emailPaciente) {
        this.emailPaciente = emailPaciente;
    }

    public Character getSexo() {
        return sexo;
    }

    public void setSexo(Character sexo) {
        this.sexo = sexo;
    }

    public Date getFecNac() {
        return fecNac;
    }

    public void setFecNac(Date fecNac) {
        this.fecNac = fecNac;
    }

    public Short getActivo() {
        return activo;
    }

    public void setActivo(Short activo) {
        this.activo = activo;
    }

    @XmlTransient
    public Collection<EntradaFicha> getEntradaFichaCollection() {
        return entradaFichaCollection;
    }

    public void setEntradaFichaCollection(Collection<EntradaFicha> entradaFichaCollection) {
        this.entradaFichaCollection = entradaFichaCollection;
    }

    @XmlTransient
    public Collection<AtencionAgen> getAtencionAgenCollection() {
        return atencionAgenCollection;
    }

    public void setAtencionAgenCollection(Collection<AtencionAgen> atencionAgenCollection) {
        this.atencionAgenCollection = atencionAgenCollection;
    }

    @Override
    public int hashCode() {
        int hash = 0;
        hash += (idPaciente != null ? idPaciente.hashCode() : 0);
        return hash;
    }

    @Override
    public boolean equals(Object object) {
        // TODO: Warning - this method won't work in the case the id fields are not set
        if (!(object instanceof Paciente)) {
            return false;
        }
        Paciente other = (Paciente) object;
        if ((this.idPaciente == null && other.idPaciente != null) || (this.idPaciente != null && !this.idPaciente.equals(other.idPaciente))) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "cl.cheekibreeki.cmh.lib.dal.entities.Paciente[ idPaciente=" + idPaciente + " ]";
    }
    
}
