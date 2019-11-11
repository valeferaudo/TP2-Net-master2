<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InscripcionesDocente.aspx.cs" Inherits="UI.Web.InscripcionesDocente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <asp:Panel ID="gridPanel" runat="server">
        Inscripciones:<asp:GridView ID="gridViewInscripciones" runat="server" AutoGenerateColumns="False" >
            <Columns>
                <asp:BoundField HeaderText="ID" DataField="ID" />
                <asp:BoundField HeaderText="Curso" DataField="descripcionCurso" />
                <asp:BoundField HeaderText="Cargo" DataField="Cargo" />
                
             </Columns>
        </asp:GridView>
        <asp:Button ID="btnNuevaInscripcion" runat="server" Text="Inscribir" OnClick="btnNuevaInscripcion_Click" />
        <br />
        </asp:Panel>
        <asp:Panel ID="PanelInscribir" runat="server">
        <asp:Label ID="cursoLabel" runat="server" Text="Seleccione Curso"></asp:Label>
            <asp:DropDownList ID="ddlCurso" runat="server">
            </asp:DropDownList>
        <br />
            <asp:Label ID="Label1" runat="server" Text="Seleccione Cargo"></asp:Label>
            <asp:DropDownList ID="ddlCargo" runat="server">
            </asp:DropDownList>
        <br />
            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClick="btnAceptar_Click" />
        </asp:Panel>
</asp:Content>
