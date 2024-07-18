<%@ Page Title="" Language="C#" MasterPageFile="~/UserInterface/User.Master" AutoEventWireup="true" CodeBehind="SifreYenile.aspx.cs" Inherits="VideoPaylasimSitesi.UserInterface.SifreYenile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="site_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="site_content" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-6 offset-md-3">
                <h2>Şifre Yenile</h2>
                <form id="resetPasswordForm" runat="server">
                    <div class="mb-3">
                        <label for="txtOldPassword" class="form-label">Eski Şifre</label>
                        <asp:TextBox ID="txtOldPassword" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="txtNewPassword" class="form-label">Yeni Şifre</label>
                        <asp:TextBox ID="txtNewPassword" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="txtConfirmNewPassword" class="form-label">Yeni Şifre (Tekrar)</label>
                        <asp:TextBox ID="txtConfirmNewPassword" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnResetPassword" Text="Şifreyi Güncelle" CssClass="btn btn-primary" runat="server" OnClick="btnResetPassword_Click" />
                </form>
            </div>
        </div>
    </div>

  
</asp:Content>
