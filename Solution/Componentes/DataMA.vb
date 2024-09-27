Public Class DataMA
    Inherits Web.UI.WebControls.TextBox

#Region "Public Properties"

    Private mCampoAssociado As String
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Data() As DateTime
        Get
            Try
                Return DateTime.Parse(Me.Text, New System.Globalization.CultureInfo("pt-BR"))
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
    End Property

    Public Property CampoAssociado() As String
        Get
            Return mCampoAssociado
        End Get

        Set(ByVal value As String)
            mCampoAssociado = value
        End Set
    End Property


#End Region

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub Control_Init(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Init
        Me.MaxLength = 7 ' mm/yyyy
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="writer"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
        Me.Style.Add("text-align", "left")

        Me.Attributes.Remove("onKeyPress")
        Me.Attributes.Add("onKeyPress", Me.ID & "_onKeyPress(this, event.keyCode)")
        Me.Attributes.Remove("onBlur")
        Me.Attributes.Add("onBlur", Me.ID & "_onBlur(this)")
        MyBase.Render(writer)


        '<script language = "Javascript">
        '/**
        ' */
        '// Declaring valid date character, minimum year and maximum year
        'var dtCh= "/";
        'var minYear=1900;
        'var maxYear=2100;

        'function isInteger(s){
        '	var i;
        '    for (i = 0; i < s.length; i++){   
        '        // Check that current character is number.
        '        var c = s.charAt(i);
        '        if (((c < "0") || (c > "9"))) return false;
        '    }
        '    // All characters are numbers.
        '    return true;
        '}

        'function stripCharsInBag(s, bag){
        '	var i;
        '    var returnString = "";
        '    // Search through string's characters one by one.
        '    // If character is not in bag, append to returnString.
        '    for (i = 0; i < s.length; i++){   
        '        var c = s.charAt(i);
        '        if (bag.indexOf(c) == -1) returnString += c;
        '    }
        '    return returnString;
        '}

        'function daysInFebruary (year){
        '	// February has 29 days in any year evenly divisible by four,
        '    // EXCEPT for centurial years which are not also divisible by 400.
        '    return (((year % 4 == 0) && ( (!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28 );
        '}
        'function DaysArray(n) {
        '	for (var i = 1; i <= n; i++) {
        '            this [i] = 31
        '		if (i==4 || i==6 || i==9 || i==11) {this[i] = 30}
        '		if (i==2) {this[i] = 29}
        '   } 
        '                    Return this
        '}

        'function isDate(dtStr){
        '        var daysInMonth = DaysArray(12)
        '        var pos1 = dtStr.indexOf(dtCh)
        '        var pos2 = dtStr.indexOf(dtCh, pos1 + 1)
        '        var strMonth = dtStr.substring(0, pos1)
        '        var strDay = dtStr.substring(pos1 + 1, pos2)
        '        var strYear = dtStr.substring(pos2 + 1)
        '        strYr = strYear
        '	if (strDay.charAt(0)=="0" && strDay.length>1) strDay=strDay.substring(1)
        '	if (strMonth.charAt(0)=="0" && strMonth.length>1) strMonth=strMonth.substring(1)
        '	for (var i = 1; i <= 3; i++) {
        '		if (strYr.charAt(0)=="0" && strYr.length>1) strYr=strYr.substring(1)
        '	}
        '                        month = parseInt(strMonth)
        '                        day = parseInt(strDay)
        '                        year = parseInt(strYr)
        '	if (pos1==-1 || pos2==-1){
        '                            alert("The date format should be : mm/dd/yyyy")
        '                            Return False
        '	}
        '	if (strMonth.length<1 || month<1 || month>12){
        '                                alert("Please enter a valid month")
        '                                Return False
        '	}
        '	if (strDay.length<1 || day<1 || day>31 || (month==2 && day>daysInFebruary(year)) || day > daysInMonth[month]){
        '                                    alert("Please enter a valid day")
        '                                    Return False
        '	}
        '	if (strYear.length != 4 || year==0 || year<minYear || year>maxYear){
        '                                        alert("Please enter a valid 4 digit year between " + minYear + " and " + maxYear)
        '                                        Return False
        '	}
        '	if (dtStr.indexOf(dtCh,pos2+1)!=-1 || isInteger(stripCharsInBag(dtStr, dtCh))==false){
        '                                            alert("Please enter a valid date")
        '                                            Return False
        '	}
        '                                            Return True
        '}

        'function ValidateForm(){
        '        var dt = document.frmSample.txtDate
        '	if (isDate(dt.value)==false){
        '            dt.focus()
        '            Return False
        '	}
        '            Return True
        ' }

        '</script>



        'writer.Write(ControlChars.CrLf)
        ''text/
        'writer.Write("<script type=""javascript"">" & ControlChars.CrLf)
        ''// Declaring valid date character, minimum year and maximum year
        'writer.Write("var dtCh= ""/"";" & ControlChars.CrLf)
        'writer.Write("var minYear=1900;" & ControlChars.CrLf)
        'writer.Write("var maxYear=2100;" & ControlChars.CrLf)

        'writer.Write("function isInteger(s){" & ControlChars.CrLf)
        'writer.Write("	var i;" & ControlChars.CrLf)
        'writer.Write("    for (i = 0; i < s.length; i++){   " & ControlChars.CrLf)
        ''// Check that current character is number." 
        'writer.Write("        var c = s.charAt(i);" & ControlChars.CrLf)
        'writer.Write("        if (((c < ""0"") || (c > ""9""))) return false;" & ControlChars.CrLf)
        'writer.Write("    }" & ControlChars.CrLf)
        ''// All characters are numbers.
        'writer.Write("    return true;" & ControlChars.CrLf)
        'writer.Write("}" & ControlChars.CrLf)

        'writer.Write("function stripCharsInBag(s, bag){" & ControlChars.CrLf)
        'writer.Write("	var i;" & ControlChars.CrLf)
        'writer.Write("    var returnString = "";" & ControlChars.CrLf)
        ''// Search through string's characters one by one.
        ''// If character is not in bag, append to returnString.
        'writer.Write("    for (i = 0; i < s.length; i++){   " & ControlChars.CrLf)
        'writer.Write("        var c = s.charAt(i);" & ControlChars.CrLf)
        'writer.Write("        if (bag.indexOf(c) == -1) returnString += c;" & ControlChars.CrLf)
        'writer.Write("    }" & ControlChars.CrLf)
        'writer.Write("    return returnString;" & ControlChars.CrLf)
        'writer.Write("}" & ControlChars.CrLf)

        'writer.Write("function daysInFebruary (year){" & ControlChars.CrLf)
        ''// February has 29 days in any year evenly divisible by four,
        ''// EXCEPT for centurial years which are not also divisible by 400.
        'writer.Write("    return (((year % 4 == 0) && ( (!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28 );" & ControlChars.CrLf)
        'writer.Write("}" & ControlChars.CrLf)

        'writer.Write("function DaysArray(n) {" & ControlChars.CrLf)
        'writer.Write("	for (var i = 1; i <= n; i++) {" & ControlChars.CrLf)
        'writer.Write("    this [i] = 31" & ControlChars.CrLf)
        'writer.Write("		if (i==4 || i==6 || i==9 || i==11) {this[i] = 30}" & ControlChars.CrLf)
        'writer.Write("		if (i==2) {this[i] = 29}" & ControlChars.CrLf)
        'writer.Write("   } " & ControlChars.CrLf)
        'writer.Write("            Return this" & ControlChars.CrLf)
        'writer.Write(" }" & ControlChars.CrLf)

        'writer.Write("function isDate(dtStr){" & ControlChars.CrLf)
        'writer.Write("var daysInMonth = DaysArray(12)" & ControlChars.CrLf)
        'writer.Write("var pos1 = dtStr.indexOf(dtCh)" & ControlChars.CrLf)
        'writer.Write("var pos2 = dtStr.indexOf(dtCh, pos1 + 1)" & ControlChars.CrLf)
        'writer.Write("var strMonth = dtStr.substring(0, pos1)" & ControlChars.CrLf)
        'writer.Write("var strDay = dtStr.substring(pos1 + 1, pos2)" & ControlChars.CrLf)
        'writer.Write("var strYear = dtStr.substring(pos2 + 1)" & ControlChars.CrLf)
        'writer.Write("strYr = strYear" & ControlChars.CrLf)
        'writer.Write("	if (strDay.charAt(0)==""0"" && strDay.length>1) strDay=strDay.substring(1)" & ControlChars.CrLf)
        'writer.Write("	if (strMonth.charAt(0)==""0"" && strMonth.length>1) strMonth=strMonth.substring(1)" & ControlChars.CrLf)
        'writer.Write("	for (var i = 1; i <= 3; i++) {" & ControlChars.CrLf)
        'writer.Write("		if (strYr.charAt(0)==""0"" && strYr.length>1) strYr=strYr.substring(1)" & ControlChars.CrLf)
        'writer.Write("	}" & ControlChars.CrLf)
        'writer.Write("                Month = parseInt(strMonth)" & ControlChars.CrLf)
        'writer.Write("                Day = parseInt(strDay)" & ControlChars.CrLf)
        'writer.Write("                Year = parseInt(strYr)" & ControlChars.CrLf)
        'writer.Write("	if (pos1==-1 || pos2==-1){" & ControlChars.CrLf)
        'writer.Write("                    alert(""The date format should be : mm/dd/yyyy"")" & ControlChars.CrLf)
        'writer.Write("                    Return False" & ControlChars.CrLf)
        'writer.Write("	}" & ControlChars.CrLf)
        'writer.Write("	if (strMonth.length<1 || month<1 || month>12){" & ControlChars.CrLf)
        'writer.Write("                        alert(""Please enter a valid month"")" & ControlChars.CrLf)
        'writer.Write("                        Return False" & ControlChars.CrLf)
        'writer.Write("	}" & ControlChars.CrLf)
        'writer.Write("	if (strDay.length<1 || day<1 || day>31 || (month==2 && day>daysInFebruary(year)) || day > daysInMonth[month]){" & ControlChars.CrLf)
        'writer.Write("                            alert(""Please enter a valid day"")" & ControlChars.CrLf)
        'writer.Write("                            Return False" & ControlChars.CrLf)
        'writer.Write("	}" & ControlChars.CrLf)
        'writer.Write("	if (strYear.length != 4 || year==0 || year<minYear || year>maxYear){" & ControlChars.CrLf)
        'writer.Write("                                alert(""Please enter a valid 4 digit year between "" + minYear + "" and "" + maxYear)" & ControlChars.CrLf)
        'writer.Write("                                Return False" & ControlChars.CrLf)
        'writer.Write("	}" & ControlChars.CrLf)
        'writer.Write("	if (dtStr.indexOf(dtCh,pos2+1)!=-1 || isInteger(stripCharsInBag(dtStr, dtCh))==false){" & ControlChars.CrLf)
        'writer.Write("                                    alert(""Please enter a valid date"")" & ControlChars.CrLf)
        'writer.Write("                                    Return False" & ControlChars.CrLf)
        'writer.Write("	}" & ControlChars.CrLf)
        'writer.Write("                                    Return True" & ControlChars.CrLf)
        'writer.Write("}" & ControlChars.CrLf)

        'writer.Write("function ValidateForm(){" & ControlChars.CrLf)
        'writer.Write("var dt = document.frmSample.txtDate" & ControlChars.CrLf)
        'writer.Write("	if (isDate(dt.value)==false){" & ControlChars.CrLf)
        'writer.Write("dt.focus()" & ControlChars.CrLf)
        'writer.Write("    Return False" & ControlChars.CrLf)
        'writer.Write("	}" & ControlChars.CrLf)
        'writer.Write("    Return True" & ControlChars.CrLf)
        'writer.Write("}" & ControlChars.CrLf)
        'writer.Write("</script>" & ControlChars.CrLf)



        writer.Write(ControlChars.CrLf)
        writer.Write("<script type=""text/javascript"">" & ControlChars.CrLf)
        writer.Write("function " & Me.ID & "_onKeyPress(componente, tecla) {" & ControlChars.CrLf)

        writer.Write("    var barras = 0;" & ControlChars.CrLf)

        writer.Write("    for (i = 0; i < componente.value.length; i++) {" & ControlChars.CrLf)
        writer.Write("        if (componente.value.slice(i,i+1) == ""/"")" & ControlChars.CrLf)
        writer.Write("            barras++;" & ControlChars.CrLf)
        writer.Write("    }" & ControlChars.CrLf)

        writer.Write("    if ((tecla == 47 && barras > 1) || (tecla != 47 && (tecla < 48 || tecla > 57)))" & ControlChars.CrLf)
        writer.Write("        tecla = 0;" & ControlChars.CrLf)

        writer.Write("    event.keyCode = tecla;" & ControlChars.CrLf)
        writer.Write("}" & ControlChars.CrLf)

        writer.Write("function " & Me.ID & "_onBlur(componente) {" & ControlChars.CrLf)
        writer.Write("    var dia = 1, mes = 0, ano = 0;" & ControlChars.CrLf)
        writer.Write("    var barras = 0;" & ControlChars.CrLf)
        writer.Write("    var data = """";" & ControlChars.CrLf)
        writer.Write("    var mensagem = """";" & ControlChars.CrLf)

        writer.Write("    if (componente.value.length == 0)" & ControlChars.CrLf)
        writer.Write("        mensagem = ""Data Inválida!!!"";" & ControlChars.CrLf)


        writer.Write("    for (i = 0; i < componente.value.length; i++) {" & ControlChars.CrLf)
        writer.Write("        if (componente.value.slice(i,i+1) == ""/"")" & ControlChars.CrLf)
        writer.Write("            barras++;" & ControlChars.CrLf)
        writer.Write("        else" & ControlChars.CrLf)
        writer.Write("            data += componente.value.slice(i,i+1);" & ControlChars.CrLf)
        writer.Write("    }" & ControlChars.CrLf)

        '        writer.Write("    if ((barras != 0 && barras != 2) || (data.length != 6 && data.length != 8)) {" & ControlChars.CrLf)
        writer.Write("    if ((barras != 0 && barras != 2) || (data.length != 4 && data.length != 6)) {" & ControlChars.CrLf)
        writer.Write("        window.alert(""Data inválida!"");" & ControlChars.CrLf)
        writer.Write("        componente.focus();" & ControlChars.CrLf)
        writer.Write("        return;" & ControlChars.CrLf)
        writer.Write("    } else {" & ControlChars.CrLf)
        'writer.Write("        dia = data.slice(0,2);" & ControlChars.CrLf)
        'writer.Write("        mes = data.slice(2,4);" & ControlChars.CrLf)
        'writer.Write("        ano = data.slice(4);" & ControlChars.CrLf)

        'writer.Write("        dia = data.slice(0,2);" & ControlChars.CrLf)
        writer.Write("        mes = data.slice(0,2);" & ControlChars.CrLf)
        writer.Write("        ano = data.slice(2,6);" & ControlChars.CrLf)

        writer.Write("    }" & ControlChars.CrLf)

        writer.Write("    if (ano < 100)" & ControlChars.CrLf)
        writer.Write("        ano = ""20"" + ano;" & ControlChars.CrLf)
        writer.Write("    else if (ano < 1000)" & ControlChars.CrLf)
        writer.Write("        ano = ""2"" + ano;" & ControlChars.CrLf)


        writer.Write("    if (ano.length != 2 && ano.length != 4) {" & ControlChars.CrLf)
        writer.Write("        mensagem = ""Ano Inválido"";" & ControlChars.CrLf)
        '        writer.Write("    } else if (dia < 1 || dia > 31) {" & ControlChars.CrLf)
        '       writer.Write("        mensagem = ""Dia Inválido."";" & ControlChars.CrLf)
        writer.Write("    } else if (mes < 1 || mes > 12) {" & ControlChars.CrLf)
        writer.Write("        mensagem = ""Mes Inválido."";" & ControlChars.CrLf)
        writer.Write("    } else if (ano <= 1800) {" & ControlChars.CrLf)
        writer.Write("        mensagem = ""Ano deve ser maior que 1800."";" & ControlChars.CrLf)
        writer.Write("    } else if (dia > 30 && (mes == 04 || mes == 06 || mes == 09 || mes == 11)) {" & ControlChars.CrLf)
        writer.Write("        mensagem = ""Dia Inválido para o mes "" + mes + ""."";" & ControlChars.CrLf)
        '        writer.Write("    } else if (mes == 2 && dia > 29) {" & ControlChars.CrLf)
        '       writer.Write("        mensagem = ""Dia Inválido para fevereiro."";" & ControlChars.CrLf)
        '      writer.Write("    } else if (dia == 29 && mes == 2 && (ano % 4 != 0)) {" & ControlChars.CrLf)
        '     writer.Write("        mensagem = ""Dia Inválido"";" & ControlChars.CrLf)
        writer.Write("    }" & ControlChars.CrLf)

        writer.Write("    try {" & ControlChars.CrLf)
        writer.Write("        new Date(ano, mes, 1)" & ControlChars.CrLf)
        'writer.Write("        componente.value = dia + ""/"" + mes + ""/"" + ano;" & ControlChars.CrLf)
        writer.Write("        componente.value =  mes + ""/"" + ano;" & ControlChars.CrLf)
        writer.Write("    } catch(err) {" & ControlChars.CrLf)
        writer.Write("        mensagem = ""Mes Ano Inválido!!!"";" & ControlChars.CrLf)
        writer.Write("    }" & ControlChars.CrLf)

        writer.Write("    if (mensagem.length != 0) {" & ControlChars.CrLf)
        writer.Write("        window.alert(mensagem);" & ControlChars.CrLf)
        writer.Write("        componente.focus();" & ControlChars.CrLf)
        writer.Write("    }" & ControlChars.CrLf)

        If Not CampoAssociado Is Nothing Then
            writer.Write(" " & CampoAssociado & ".value = componente.value; " & ControlChars.CrLf)
        End If

        writer.Write("}" & ControlChars.CrLf)
        writer.Write("</script>" & ControlChars.CrLf)

    End Sub

End Class