<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Inscripciones.aspx.cs" Inherits="UI.Web.Inscripciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">

        <div>
            <br />
            <br />
            <br />
            Inscripciones del alumno
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        </div>
        <asp:GridView ID="GridViewInscripciones" runat="server" AllowCustomPaging="True" SelectedRowStyle-BackColor="Black" 
            SelectedRowStyle-ForeColor="White" DataKeyNames="ID" OnSelectedIndexChanged="gridView_SelectedIndexChanged" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField HeaderText="ID" DataField="ID" />
                <asp:BoundField HeaderText="Materia" DataField="Materia" />
                <asp:BoundField HeaderText="Comision" DataField="Comision" />
                <asp:BoundField HeaderText="Año calendario" DataField="aniocal" />
                <asp:BoundField HeaderText="Curso" DataField="curso" />
                <asp:BoundField HeaderText="Condicion" DataField="condicion" />
                <asp:BoundField HeaderText="Nota" DataField="nota" />
                <asp:CommandField SelectText="Seleccionar" ShowSelectButton="true" />
            </Columns>
        </asp:GridView>
        <asp:Button ID="btnNuevaInscripcion" runat="server" Text="Inscribir" OnClick="btnNuevaInscripcion_Click" />
        <asp:Button ID="btnCalificar" runat="server" Text="Calificar" OnClick="btnCalificar_Click" />
        <asp:Panel ID="Panel1" runat="server">
            Seleccione un curso :
            <asp:DropDownList ID="DropDownList1" runat="server">
            </asp:DropDownList>
            <asp:Button ID="btnInscribir" runat="server" Text="Inscribir" OnClick="btnInscribir_Click" />
        </asp:Panel>
        <asp:Panel ID="Panel2" runat="server">
            Ingrese calificacion :
            <asp:TextBox ID="TextBox1" runat="server" Width="40px"></asp:TextBox>
            &nbsp;Condicion :
            <asp:DropDownList ID="DropDownList2" runat="server">
                <asp:ListItem>Regular</asp:ListItem>
                <asp:ListItem>Libre</asp:ListItem>
                <asp:ListItem>Aprobado</asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="btnGuardarCalificacion" runat="server" OnClick="btnGuardarCalificacion_Click" Text="Calificar" />
        </asp:Panel>
    
</asp:Content>