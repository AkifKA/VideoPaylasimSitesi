<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.Master" AutoEventWireup="true" CodeBehind="SifreYenile.aspx.cs" Inherits="VideoPaylasimSitesi.AdminPanel.SifreYenile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <form id="form1" runat="server">
        <div class="container">
            <div class="row justify-content-center">
                <div class="col-md-6">
                    <h2>Şifre Yenile</h2>
                    <asp:Panel ID="resetPasswordForm" runat="server">
                        <div class="mb-3">
                            <label for="txtCurrentPassword" class="form-label">Mevcut Şifre</label>
                            <asp:TextBox ID="txtCurrentPassword" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="txtNewPassword" class="form-label">Yeni Şifre</label>
                            <asp:TextBox ID="txtNewPassword" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="txtConfirmNewPassword" class="form-label">Yeni Şifre (Tekrar)</label>
                            <asp:TextBox ID="txtConfirmNewPassword" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox>
                        </div>
                        <asp:Button ID="btnResetPassword" Text="Şifreyi Yenile" CssClass="btn btn-primary" runat="server" OnClick="btnResetPassword_Click" />
                    </asp:Panel>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
