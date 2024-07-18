<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.Master" AutoEventWireup="true" CodeBehind="Videolar.aspx.cs" Inherits="VideoPaylasimSitesi.AdminPanel.Videolar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <h2 class="text-center">Videolar</h2>
    <% 
        if (Session["yoneticiYetki"] == null)
        {
    %>
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
        Rolünüz uygun değildir. Lütfen uygun rol ile giriş yapınız.
    </div>
    <div class="text-center">
        <asp:HyperLink CssClass="btn btn-primary" NavigateUrl="~/AdminPanel/Giris.aspx" Text="Giriş Yap" runat="server" />
    </div>
    <% 
    }
    else
    { %>
    <div class="d-flex justify-content-end">
        <button type="button" class="btn btn-success mb-4" data-toggle="modal" data-target="#videoEkleModal">Ekle</button>
    </div>
    <table class="table">
        <thead>
            <tr>
                <th scope="col">ID</th>
                <th scope="col">Video Başlığı</th>
                <th scope="col">Video Açıklaması</th>
                <th scope="col">Video Resim URL</th>
                <th scope="col">Video Frame URL</th>
                <th scope="col">Alt Kategorisi</th>
                <th scope="col">Üst Kategorisi</th>
                <th scope="col">İşlemler</th>
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
                        <td><%# Eval("KatAd") %></td>
                        <td>
                            <a href='<%# ResolveUrl("~/AdminPanel/VideoGuncelle.aspx?VideoID=" + Eval("VideoID")) %>'>Düzenle</a> | 
                            <a href='<%# "Sil.aspx?id=" + Eval("VideoID") + "&sorgu=DELETE FROM Videolar WHERE VideoID = @ID&type=video" %>'
                                onclick="return confirm('Bu videoyu silmek istediğinizden emin misiniz?')">Sil</a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>



    <h2 class="text-center">Video Beğeni Sayıları</h2>
    <table class="table">
        <thead>
            <tr>
                <th scope="col">Video ID</th>
                <th scope="col">Video Başlığı</th>
                <th scope="col">Alt Kategori Adı</th>
                <th scope="col">Üst Kategori Adı</th>
                <th scope="col">Beğeni Sayısı</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="rptVideoBegeniSayilari" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("VideoID") %></td>
                        <td><%# Eval("VideoBaslik") %></td>
                        <td><%# Eval("AltKatAd") %></td>
                        <td><%# Eval("KatAd") %></td>
                        <td><%# Eval("BegeniSayisi") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr>
                        <td colspan="5" class="text-center font-weight-bold">Toplam Beğeni Sayısı: <%= ToplamBegeniSayisi %></td>
                    </tr>
                </FooterTemplate>
            </asp:Repeater>
        </tbody>
    </table>

    <h2 class="text-center">Beğeniler</h2>
    <table class="table">
        <thead>
            <tr>
                <th scope="col">Kullanıcı Adı</th>
                <th scope="col">Video Başlığı</th>
                <th scope="col">Beğeni Durumu</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="rptBegeniler" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("KullaniciTakmaAd") %></td>
                        <td><%# Eval("VideoBaslik") %></td>
                        <td><%# Convert.ToBoolean(Eval("BegeniDurumu")) ? "Beğenildi" : "Beğenilmedi" %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>




    <!-- Video Ekle Modal -->
    <div class="modal fade" id="videoEkleModal" tabindex="-1" role="dialog" aria-labelledby="videoEkleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="videoEkleModalLabel">Video Ekle</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
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
                            <asp:Label AssociatedControlID="DrpVideoKategoriAdi" Text="Video Kategorisi" runat="server" CssClass="form-label" />
                            <asp:DropDownList ID="DrpVideoKategoriAdi" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </div>
                        <asp:Button ID="BtnVideoEkle" Text="Ekle" CssClass="btn btn-primary mb-3" runat="server" OnClick="BtnVideoEkle_Click" />
                    </form>
                </div>
            </div>
        </div>
    </div>
    <% } %>
</asp:Content>
