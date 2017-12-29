<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
    Inherits="System.Web.Mvc.ViewPage<IEnumerable<Crimson.Catalog.Resources._1000NovelsEveryOneMustRead>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	1000Novels
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>1000Novels</h2>

    <table>
        <tr>
            <th></th>
        </tr>

    <% foreach (var item in this.) { %>
    
        <tr>
            <td>
                <%= Html.Encode(item.) %>
                <%= Html.ActionLink("Edit", "Edit", new { /* id=item.PrimaryKey */ }) %> |
                <%= Html.ActionLink("Details", "Details", new { /* id=item.PrimaryKey */ })%>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>

</asp:Content>

