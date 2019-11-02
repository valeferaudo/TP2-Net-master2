<%@ Page Title="Alumnos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Alumnos.aspx.cs" Inherits="UI.Web.Alumnos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">

    
        <div>
            Alumnos</div>
        <asp:GridView ID="GridViewAlumnos" runat="server" AutoGenerateColumns="false" SelectedRowStyle-BackColor="Black" 
            SelectedRowStyle-ForeColor="White" DataKeyNames="ID" OnSelectedIndexChanged="gridView_SelectedIndexChanged">
            <Columns>
                <asp:BoundField HeaderText="ID" DataField="ID" />
                <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                <asp:BoundField HeaderText="Apellido" DataField="Apellido" />
                <asp:BoundField HeaderText="Direccion" DataField="Direccion" />
                <asp:BoundField HeaderText="Email" DataField="Email" />
                <asp:BoundField HeaderText="Telefono" DataField="Telefono" />
                <asp:BoundField HeaderText="Fecha de Nacimiento" DataField="FechaNac" />
                <asp:BoundField HeaderText="Legajo" DataField="Legajo" />
                <asp:BoundField HeaderText="Plan" DataField="Plan" />
                <asp:CommandField SelectText="Seleccionar" ShowSelectButton="true" />
            </Columns>
        </asp:GridView>
        <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click" />
        <asp:Button ID="btnEditar" runat="server" Text="Editar" OnClick="btnEditar_Click" />
        <asp:Button ID="btnEliminar" runat="server" OnClick="Button3_Click" Text="Eliminar" />
        <asp:Button ID="btnInscripciones" runat="server" Text="Inscripciones" />
        <asp:Panel ID="Panel1" runat="server">
            <table style="width:100%;">
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Nombre :</td>
                    <td>
                        <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Apellido :</td>
                    <td>
                        <asp:TextBox ID="txtApellido" runat="server"></asp:TextBox>
                    </td>
                    

                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Direccion :</td>
                    <td>
                        <asp:TextBox ID="txtDireccion" runat="server"></asp:TextBox>
                    </td>
                    

                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Email :</td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    </td>
                    

                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Telefono :</td>
                    <td>
                        <asp:TextBox ID="txtTelefono" runat="server"></asp:TextBox>
                    </td>
                    

                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Fecha de nacimiento :</td>
                    <td>
                        <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
                    </td>
                    

                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Legajo :</td>
                    <td>
                        <asp:TextBox ID="txtLegajo" runat="server"></asp:TextBox>
                    </td>
                    

                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Plan :</td>
                    <td>
                        <asp:DropDownList ID="DropDownListPlan" runat="server">
                        </asp:DropDownList>
                    </td>
                    

                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" Text="Guardar" />
                    </td>
                    

                    <td>&nbsp;</td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="Panel2" runat="server">
        </asp:Panel>
    

    </asp:Content>
