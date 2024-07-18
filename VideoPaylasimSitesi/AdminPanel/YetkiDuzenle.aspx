<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.Master" AutoEventWireup="true" CodeBehind="YetkiDuzenle.aspx.cs" Inherits="VideoPaylasimSitesi.AdminPanel.YetkiDuzenle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <h2 class="text-center">Yönetici Yetki Düzenle</h2>
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
        else if (Session["yoneticiYetki"].ToString() != "Süper Yönetici")
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
            <label for="TxtYoneticiAdi" class="form-label">Yönetici Adı</label>
            <asp:TextBox ID="TxtYoneticiAdi" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="TxtYoneticiSoyadi" class="form-label">Yönetici Soyadı</label>
            <asp:TextBox ID="TxtYoneticiSoyadi" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="TxtYoneticiYetki" class="form-label">Yönetici Yetkisi</label>
            <asp:TextBox ID="TxtYoneticiYetki" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="DrpYoneticiYeniYetki" class="form-label">Yönetici Yeni Yetkisi</label>
            <asp:DropDownList ID="DrpYoneticiYeniYetki" CssClass="form-control" runat="server">
                <asp:ListItem Text="--Seçiniz--" Value=""></asp:ListItem>
                <asp:ListItem Text="Süper Yönetici" Value="SuperYonetici"></asp:ListItem>
                <asp:ListItem Text="Video Düzenleyici" Value="Video Düzenleyici"></asp:ListItem>
                <asp:ListItem Text="Yorum Düzenleyici" Value="Yorum Düzenleyici"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <asp:Button ID="BtnYetkiDuzenle" Text="Güncelle" CssClass="btn btn-primary" runat="server" OnClick="BtnYetkiDuzenle_Click1" />
    </form>
         <% } %>
</asp:Content>
