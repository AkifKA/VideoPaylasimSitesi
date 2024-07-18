<%@ Page Title="" Language="C#" MasterPageFile="~/UserInterface/User.Master" AutoEventWireup="true" CodeBehind="Profil.aspx.cs" Inherits="VideoPaylasimSitesi.UserInterface.Profil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="site_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="site_content" runat="server">
    <div class="container">
        <div class="row">
            <!-- Kullanıcı Bilgileri Görüntüleme Bölümü -->
            <div class="col-md-6">
                <h2>Kullanıcı Bilgileri</h2>
                <div class="card mb-3">
                    <div class="card-body">
                        <asp:Image ID="imgProfilePicture" CssClass="img-thumbnail mb-3" runat="server" />
                        <p><strong>Ad:</strong> <asp:Literal ID="litKullaniciAdi" runat="server"></asp:Literal></p>
                        <p><strong>Soyad:</strong> <asp:Literal ID="litKullaniciSoyadi" runat="server"></asp:Literal></p>
                        <p><strong>Takma Ad:</strong> <asp:Literal ID="litKullaniciTakmaAd" runat="server"></asp:Literal></p>
                        <p><strong>Email:</strong> <asp:Literal ID="litKullaniciEmail" runat="server"></asp:Literal></p>
                    </div>
                </div>
            </div>

            <!-- Kullanıcı Bilgileri Güncelleme Bölümü -->
            <div class="col-md-6">
                <h2>Bilgileri Güncelle</h2>
                <form id="updateForm" runat="server">
                    <div class="mb-3">
                        <label for="txtAdi" class="form-label">Ad</label>
                        <asp:TextBox ID="txtAdi" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="txtSoyadi" class="form-label">Soyad</label>
                        <asp:TextBox ID="txtSoyadi" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="txtTakmaAd" class="form-label">Takma Ad</label>
                        <asp:TextBox ID="txtTakmaAd" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="txtEmail" class="form-label">Email</label>
                        <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="fileUpload" class="form-label">Profil Resmi</label>
                        <asp:FileUpload ID="fileUpload" CssClass="form-control" runat="server" />
                    </div>
                    <asp:Button ID="btnUpdate" Text="Güncelle" CssClass="btn btn-primary" runat="server" OnClick="btnUpdate_Click" />
                </form>
            </div>
        </div>
    </div>
</asp:Content>
