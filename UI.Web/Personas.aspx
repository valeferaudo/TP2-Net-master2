<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Personas.aspx.cs" Inherits="UI.Web.Personas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <asp:Panel ID="gridPanel" runat="server">
        Personas:<asp:GridView ID="gridViewPersonas" runat="server" AutoGenerateColumns="False" SelectedRowStyle-BackColor="Black" 
            SelectedRowStyle-ForeColor="White" DataKeyNames="ID" OnSelectedIndexChanged="gridView_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Apellido" HeaderText="Nombre" />
                <asp:BoundField DataField="Direccion" HeaderText="Direccion" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="Telefono" HeaderText="Telefono" />
                <asp:BoundField DataField="FechaNacimiento" HeaderText="Fecha Nacimiento" />
                <asp:BoundField DataField="Legajo" HeaderText="Legajo" />
                <asp:BoundField DataField="TipoPersona" HeaderText="Tipo Persona" />
                <asp:BoundField DataField="Plan" HeaderText="Plan" />
                <asp:CommandField SelectText="Seleccionar" ShowSelectButton="true" />
            </Columns>
        </asp:GridView>
        <asp:Panel ID="gridActionsPanel" runat="server">
            <asp:LinkButton ID="editarLinkButton" runat="server" OnClick="editarLinkButton_Click">Editar</asp:LinkButton>
            <asp:LinkButton ID="eliminarLinkButton" runat="server" OnClick="eliminarLinkButton_Click">Eliminar</asp:LinkButton>
            <asp:LinkButton ID="nuevoLinkButton" runat="server" OnClick="nuevoLinkButton_Click">Nuevo</asp:LinkButton>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="formPanel" Visible="false" runat="server">
        <asp:Label ID="nombreLabel" runat="server" Text="Nombre"></asp:Label>
        <asp:TextBox ID="nombreTextBox" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="nombreTextBox" ErrorMessage="Ingrese Nombre" ForeColor="Red">*</asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="apellidoLabel" runat="server" Text="Apellido"></asp:Label>
        <asp:TextBox ID="apellidoTextBox" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="apellidoTextBox" ErrorMessage="Ingrese Apellido" ForeColor="Red">*</asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="direccionLabel" runat="server" Text="Direccion"></asp:Label>
        <asp:TextBox ID="direccionTextBox" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="direccionTextBox" ErrorMessage="Ingrese Direccion" ForeColor="Red">*</asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="emailLabel" runat="server" Text="Email"></asp:Label>
        <asp:TextBox ID="emailTextBox" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="emailTextBox" ErrorMessage="Ingrese Mail" ForeColor="Red">*</asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Ingrese Mail correcto" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="emailTextBox" ForeColor="Red">*</asp:RegularExpressionValidator>
        <br />
        <asp:Label ID="telefonoLabel" runat="server" Text="Telefono"></asp:Label>
        <asp:TextBox ID="telefonoTextBox" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="telefonoTextBox" ErrorMessage="Ingrese telefono" ForeColor="Red">*</asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Ingrese telefono correcto" ValidationExpression="^\d+$" ControlToValidate="telefonoTextBox" ForeColor="Red">*</asp:RegularExpressionValidator>
        <br />
        <asp:Label ID="fechanacLabel" runat="server" Text="Fecha Nacimiento"></asp:Label>
        <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
        <br />
        <asp:Label ID="legajoLabel" runat="server" Text="Legajo"></asp:Label>
        <asp:TextBox ID="legajoTextBox" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="legajoTextBox" ErrorMessage="Ingrese legajo" ForeColor="Red">*</asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Ingrese legajo correcto" ValidationExpression="^\d+$" ControlToValidate="legajoTextBox" ForeColor="Red">*</asp:RegularExpressionValidator>
        <br />
        <asp:Label ID="tipoLabel" runat="server" Text="Tipo Persona"></asp:Label>
        <asp:DropDownList ID="ddlTipoPersona" runat="server">
        </asp:DropDownList>
        <br />
        <asp:Label ID="idPlanLabel" runat="server" Text="Plan"></asp:Label>
        <asp:DropDownList ID="ddlPlan" runat="server">
        </asp:DropDownList>
        <br />
        <asp:Panel ID="formActionsPanel" runat="server">
            <asp:LinkButton ID="aceptarLinkButton" runat="server" OnClick="aceptarLinkButton_Click">Aceptar</asp:LinkButton>
            <asp:LinkButton ID="cancelarLinkButton" runat="server" OnClick="cancelarLinkButton_Click" CausesValidation="False">Cancelar</asp:LinkButton>
        </asp:Panel>
    </asp:Panel>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
</asp:Content>