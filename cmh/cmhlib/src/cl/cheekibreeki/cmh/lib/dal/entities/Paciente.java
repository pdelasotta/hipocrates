/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package cl.cheekibreeki.cmh.lib.dal.entities;

import java.io.Serializable;
import java.util.Collection;
import javax.persistence.Basic;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.OneToMany;
import javax.persistence.Table;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlTransient;

/**
 *
 * @author pdelasotta
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
    @NamedQuery(name = "Paciente.findByHashedPass", query = "SELECT p FROM Paciente p WHERE p.hashedPass = :hashedPass"),
    @NamedQuery(name = "Paciente.findByEmailPaciente", query = "SELECT p FROM Paciente p WHERE p.emailPaciente = :emailPaciente"),
    @NamedQuery(name = "Paciente.findByDigitoVerificador", query = "SELECT p FROM Paciente p WHERE p.digitoVerificador = :digitoVerificador")})
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
    @Column(name = "RUT")
    private Integer rut;
    @Column(name = "HASHED_PASS")
    private String hashedPass;
    @Column(name = "EMAIL_PACIENTE")
    private String emailPaciente;
    @Basic(optional = false)
    @Column(name = "DIGITO_VERIFICADOR")
    private Character digitoVerificador;
    @OneToMany(mappedBy = "idPaciente")
    private Collection<Ficha> fichaCollection;
    @OneToMany(mappedBy = "idPaciente")
    private Collection<AtencionAgen> atencionAgenCollection;

    public Paciente() {
    }

    public Paciente(Integer idPaciente) {
        this.idPaciente = idPaciente;
    }

    public Paciente(Integer idPaciente, Character digitoVerificador) {
        this.idPaciente = idPaciente;
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

    public Integer getRut() {
        return rut;
    }

    public void setRut(Integer rut) {
        this.rut = rut;
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

    public Character getDigitoVerificador() {
        return digitoVerificador;
    }

    public void setDigitoVerificador(Character digitoVerificador) {
        this.digitoVerificador = digitoVerificador;
    }

    @XmlTransient
    public Collection<Ficha> getFichaCollection() {
        return fichaCollection;
    }

    public void setFichaCollection(Collection<Ficha> fichaCollection) {
        this.fichaCollection = fichaCollection;
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