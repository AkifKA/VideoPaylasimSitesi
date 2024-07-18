<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="VideoPaylasimSitesi.AdminPanel.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">

    <% if (Session["YoneticiID"] != null)
        { %>
    <h3 class="text-center">Video Paylaşım Sitesi Admin Paneli</h3>
    <p class="text-center">Hoşgeldiniz
        <asp:Label ID="lblYoneticiAd" runat="server" Text=""></asp:Label>, yetkiniz:
        <asp:Label ID="LblYetki" runat="server" Text=""></asp:Label>
        Buradan sitenizi yönetebilirsiniz.</p>

    <h4 class="text-center">Video Listesi</h4>
    <table class="table">
        <thead>
            <tr>
                <th scope="col">ID</th>
                <th scope="col">Video Başlığı</th>
                <th scope="col">Video Açıklaması</th>
                <th scope="col">Video Resim URL</th>
                <th scope="col">Video Frame URL</th>
                <th scope="col">Kategorisi</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>
                        <th scope="row"><%# Eval("VideoID") %></th>
                        <td><%# Eval("VideoBaslik") %></td>
                        <td><%# Eval("VideoAciklama") %></td>
                        <td><%# Eval("VideoUrl") %></td>
                        <td><%# Eval("VideoFrameUrl") %></td>
                        <td><%# Eval("AltKatAd") %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
    <p>Toplam Video Sayısı: 
        <asp:Label ID="LblToplamVideo" Text="" runat="server" CssClass="text-danger"> </asp:Label></p>
    <hr />
    <div class="d-flex justify-content-center">
        <asp:HyperLink CssClass="btn btn-warning mb-4" NavigateUrl="~/AdminPanel/Videolar.aspx" Text="Videoları Yönet" runat="server" />
    </div>
    <hr />

    <h4 class="text-center">Kategori Listesi</h4>
    <table class="table">
        <thead>
            <tr>
                <th scope="col">ID</th>
                <th scope="col">Kategori Adı</th>
                <th scope="col">Kategori Açıklaması</th>
                <th scope="col">Kategori Resim URL</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="Repeater3" runat="server">
                <ItemTemplate>
                    <tr>
                        <th scope="row"><%# Eval("KatID") %></th>
                        <td><%# Eval("KatAd") %></td>
                        <td><%# Eval("KatAciklama") %></td>
                        <td><%# Eval("KatResUrl") %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
    <asp:Repeater runat="server" ID="sayac">
        <ItemTemplate>
            <p>Toplam Kategori Sayısı: 
                <asp:Label ID="LblToplamKategori" Text='<%# Eval("ToplamKategori") %>' runat="server" CssClass="text-danger" /></p>
        </ItemTemplate>
    </asp:Repeater>
    <%--<p>Toplam Kategori Sayısı:  <asp:Label ID="LblToplamKategori" Text="" runat="server" CssClass="text-danger"/></p>--%>
    <hr />

    <div class="d-flex justify-content-center">
        <asp:HyperLink CssClass="btn btn-warning mb-4" NavigateUrl="~/AdminPanel/Kategoriler.aspx" Text="Kategorileri Yönet" runat="server" />
    </div>
    <hr />
    <h4 class="text-center">Alt Kategori Listesi</h4>
    <table class="table">
        <thead>
            <tr>
                <th scope="col">ID</th>
                <th scope="col">Alt Kategori Adı</th>
                <th scope="col">Alt Kategori Açıklaması</th>
                <th scope="col">Üst Kategorisi</th>
                <th scope="col">Resim URL</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="Repeater2" runat="server">
                <ItemTemplate>
                    <tr>
                        <th scope="row"><%# Eval("AltKatID") %></th>
                        <td><%# Eval("AltKatAd") %></td>
                        <td><%# Eval("AltKatAciklama") %></td>
                        <td><%# Eval("KatAd") %></td>
                        <td><%# Eval("AltKatResUrl") %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
    </table>
    <p>Toplam Alt Kategori Sayısı: 
        <asp:Label ID="LblToplamAKategori" Text="" runat="server" CssClass="text-danger" /></p>
    <hr />
    <div class="d-flex justify-content-center">
        <asp:HyperLink CssClass="btn btn-warning mb-4" NavigateUrl="~/AdminPanel/AltKategoriler.aspx" Text="Alt Kategorileri Yönet" runat="server" />
    </div>
    <% }
    else
    { %>
     <div class="alert alert-danger text-center w-50 mx-auto" role="alert">
      Bu sayfaya erişim yetkiniz bulunmamaktadır.  Lütfen yönetici girişi yapınız.
      </div>
    <div class="text-center">
        <asp:HyperLink CssClass="btn btn-primary" NavigateUrl="~/AdminPanel/Giris.aspx" Text="Giriş Yap" runat="server" />
    </div>
    <% } %>
</asp:Content>
