<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.Master" AutoEventWireup="true" CodeBehind="VideoGuncelle.aspx.cs" Inherits="VideoPaylasimSitesi.AdminPanel.VideoGuncelle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
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
        <div class="form-group mb-3">
            <asp:Label AssociatedControlID="TxtVideoBaslik" Text="Video Başlığı" runat="server" CssClass="form-label" />
            <asp:TextBox ID="TxtVideoBaslik" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="form-group mb-3">
            <asp:Label AssociatedControlID="TxtVideoAciklama" Text="Video Açıklaması" runat="server" CssClass="form-label" />
            <asp:TextBox ID="TxtVideoAciklama" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="form-group mb-3">
            <asp:Label AssociatedControlID="TxtVideoUrl" Text="Video Resim URL" runat="server" CssClass="form-label" />
            <asp:TextBox ID="TxtVideoUrl" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="form-group mb-3">
            <asp:Label AssociatedControlID="TxtVideoFrameUrl" Text="Video Frame URL" runat="server" CssClass="form-label" />
            <asp:TextBox ID="TxtVideoFrameUrl" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="form-group mb-3">
            <asp:Label AssociatedControlID="DrpVideoKategoriID" Text="Video Kategorisi" runat="server" CssClass="form-label" />
            <asp:DropDownList ID="DrpVideoKategoriID" CssClass="form-control" runat="server"></asp:DropDownList>
        </div>
        <asp:Button ID="BtnVideoGuncelle" Text="Güncelle" CssClass="btn btn-primary mb-3" runat="server" OnClick="BtnVideoGuncelle_Click" />
    </form>
     <% } %>
</asp:Content>
