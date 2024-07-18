<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.Master" AutoEventWireup="true" CodeBehind="Profil.aspx.cs" Inherits="VideoPaylasimSitesi.AdminPanel.Profil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <!-- Yönetici Bilgileri Görüntüleme Bölümü -->
                <div class="col-md-6">
                    <h2>Yönetici Bilgileri</h2>
                    <div class="card mb-3">
                        <div class="card-body">
                            <asp:Image ID="imgProfilePicture" CssClass="img-thumbnail mb-3" runat="server" />
                            <p><strong>Ad:</strong> <asp:Literal ID="litYoneticiAdi" runat="server"></asp:Literal></p>
                            <p><strong>Soyad:</strong> <asp:Literal ID="litYoneticiSoyadi" runat="server"></asp:Literal></p>
                            <p><strong>Takma Ad:</strong> <asp:Literal ID="litYoneticiTakmaAd" runat="server"></asp:Literal></p>
                            <p><strong>Email:</strong> <asp:Literal ID="litYoneticiEmail" runat="server"></asp:Literal></p>
                            <p><strong>Rol:</strong> <asp:Literal ID="litYoneticiRol" runat="server"></asp:Literal></p>
                        </div>
                    </div>
                </div>

                <!-- Yönetici Bilgileri Güncelleme Bölümü -->
                <div class="col-md-6">
                    <h2>Bilgileri Güncelle</h2>
                    <asp:Panel ID="updateForm" runat="server">
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
                    </asp:Panel>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
