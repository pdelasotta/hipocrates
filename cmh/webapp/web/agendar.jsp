<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core"%>
<jsp:include page="/Agendamiento.do" />
<div class="row">
    <div class="col-md-8 col-md-offset-2">
        <form action="master.jsp?page=agendar" method="POST">
            <div class="form-group">
                <label for="tipoPrestacion">Tipo prestación</label>
                <select class="form-control" id="tipoPrestacion" name="tipoPrestacion" onchange="this.form.submit()">
                    <c:if test="${empty requestScope.tipoPrestacion}">
                        <option value='-1'>---</option>
                    </c:if>
                    <c:forEach var="tipoPrestacion" items="${requestScope.tiposPrestacion}">
                        <option value="${tipoPrestacion.getIdTipoPrestacion()}" ${tipoPrestacion.getIdTipoPrestacion() == requestScope.tipoPrestacion? 'selected' : ''}>
                            ${tipoPrestacion.getNomTipoPrest()}
                        </option>
                    </c:forEach>
                </select>
            </div>
            <c:if test="${not empty requestScope.tipoPrestacion}">
                <div class="form-group">
                    <label for="prestacion">Prestación</label>
                    <select class="form-control" id="prestacion" name="prestacion" onchange="this.form.submit()">
                        <c:if test="${empty requestScope.prestacion}">
                            <option value='-1'>---</option>
                        </c:if>
                        <c:forEach var="prestacion" items="${requestScope.prestaciones}">
                            <option value="${prestacion.getIdPrestacion()}" ${prestacion.getIdPrestacion() == requestScope.prestacion? 'selected' : ''}>
                                ${prestacion.getNomPrestacion()}
                            </option>
                        </c:forEach>
                    </select>
                </div>
            </c:if>
            <c:if test="${not empty requestScope.medicos}">
                <div class="form-group">
                    <label for="medicos">Médicos</label>
                    <select class="form-control" id="medicos" name="medico" onchange="this.form.submit()">
                        <c:if test="${empty requestScope.medico}">
                            <option value='-1'>---</option>
                        </c:if>
                        <c:forEach var="medico" items="${requestScope.medicos}">
                            <option value="${medico.getIdPersonal()}" ${medico.getIdPersonal() == requestScope.medico? 'selected' : ''}>
                                ${medico.getNombres()} ${medico.getApellidos()}
                            </option>
                        </c:forEach>
                    </select>
                </div>
            </c:if>
            <c:if test="${not empty requestScope.medico}">
                <c:if test="${not empty requestScope.fecha}">
                    <div class="form-group">
                        <label for="fecha">Fecha</label>
                        <input type="date" required name="fecha" value="${requestScope.fecha}" id="fecha" onchange="this.form.submit()"/>
                    </div>
                </c:if>
                <c:if test="${empty requestScope.fecha}">
                    <div class="form-group">
                        <label for="fecha">Fecha</label>
                        <input type="date" required name="fecha" id="fecha" onchange="this.form.submit()"/>
                    </div>
                </c:if>
            </c:if>
            <c:if test="${not empty requestScope.horas}">
                <div class="form-group">
                    <label for="hora">Horas disponibles</label>
                    <select class="form-control" id="hora" name="hora">
                        <c:if test="${empty requestScope.hora}">
                            <option value='-1'>---</option>
                        </c:if>
                        <c:if test="${not empty requestScope.horas}">
                            <c:if test="${not empty requestScope.horas.getHoras()}">
                                <c:forEach var="hora" items="${requestScope.horas.getHoras()}">
                                    <option value="${hora.getBloque().getIdBloque()}">
                                        ${hora.toString()}
                                    </option>
                                </c:forEach>
                            </c:if>
                        </c:if>
                    </select>
                </div>
            </c:if>
            <input id="btnRegistrar" type="submit" name="btnRegistrar" value="Registrar" onclick="$('#hiddenRegistrar').val('registrar');" class="btn btn-default" />
            <input type="hidden" value="" name="hiddenRegistrar" id="hiddenRegistrar"/>
        </form>  
    </div>
</div>