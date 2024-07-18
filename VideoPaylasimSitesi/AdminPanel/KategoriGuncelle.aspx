<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.Master" AutoEventWireup="true" CodeBehind="KategoriGuncelle.aspx.cs" Inherits="VideoPaylasimSitesi.AdminPanel.KategoriGuncelle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <h2 class="text-center">Kategori Güncelle</h2>
    <% 
        if (Session["yoneticiYetki"] == null)
        {%>
    <div class="alert alert-danger text-center w-50 mx-auto" role="alert">
        Bu sayfaya erişim yetkiniz bulunmamaktadır. Lütfen yönetici girişi yapınız.
    </div>
    <div class="text-center">
        <asp:HyperLink CssClass="btn btn-primary" NavigateUrl="~/AdminPanel/Giris.aspx" Text="Giriş Yap" runat="server" />
    </div>
    <% 
        }
        else if (Session["yoneticiYetki"].ToString() != "Süper Yönetici" && Session["yoneticiYetki"].ToString() != "Video Düzenleyici")
        {%>
    <div class="alert alert-danger text-center w-50 mx-auto" role="alert">
           Rolünüz bu sayfa için uygun değildir. Lütfen uygun rol ile giriş yapınız.
    </div>
    <div class="text-center">
        <asp:HyperLink CssClass="btn btn-primary" NavigateUrl="~/AdminPanel/Giris.aspx" Text="Giriş Yap" runat="server" />
    </div>
    <% 
        }
        else
        { %>
    <form runat="server">
        <div class="form-group">
            <label for="TxtKAd" class="form-label">Kategori Adı</label>
            <asp:TextBox ID="TxtKAd" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="TxtKAciklama" class="form-label">Kategori Açıklaması</label>
            <asp:TextBox ID="TxtKAciklama" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="TxtKResimUrl" class="form-label">Kategori Resim URL</label>
            <asp:TextBox ID="TxtKResimUrl" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <asp:Button ID="BtnKategoriGuncelle" Text="Güncelle" CssClass="btn btn-primary" runat="server" OnClick="BtnKategoriGuncelle_Click" />
    </form>
    <% 
        }
    %>
</asp:Content>
