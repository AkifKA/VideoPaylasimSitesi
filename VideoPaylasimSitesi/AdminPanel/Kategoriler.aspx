<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.Master" AutoEventWireup="true" CodeBehind="Kategoriler.aspx.cs" Inherits="VideoPaylasimSitesi.Admin.Kategoriler" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <h2 class="text-center">Kategoriler</h2>
    <% 
    if (Session["yoneticiYetki"] == null)
    { %>
        <div class="alert alert-danger text-center w-50 mx-auto" role="alert">
            Bu sayfaya erişim yetkiniz bulunmamaktadır. Lütfen yönetici girişi yapınız.
        </div>
        <div class="text-center">
            <asp:HyperLink CssClass="btn btn-primary" NavigateUrl="~/AdminPanel/Giris.aspx" Text="Giriş Yap" runat="server" />
        </div>
    <% 
    }
    else if (Session["yoneticiYetki"].ToString() != "Süper Yönetici" && Session["yoneticiYetki"].ToString() != "Video Düzenleyici")
    { %>
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
        <div class="d-flex justify-content-end">
            <button type="button" class="btn btn-success mb-4" data-toggle="modal" data-target="#categoryModal">Ekle</button>
        </div>
        <table class="table">
            <thead>
                <tr>
                    <th scope="col">ID</th>
                    <th scope="col">Kategori Adı</th>
                    <th scope="col">Kategori Açıklaması</th>
                    <th scope="col">Kategori Resim URL</th>
                    <th scope="col">İşlemler</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <tr>
                            <th scope="row"><%# Eval("KatID") %></th>
                            <td><%# Eval("KatAd") %></td>
                            <td><%# Eval("KatAciklama") %></td>
                            <td><%# Eval("KatResUrl") %></td>
                            <td>
                                <a href='<%# ResolveUrl("~/AdminPanel/KategoriGuncelle.aspx?KatID=" + Eval("KatID")) %>'>Düzenle</a> | 
                                <a href='<%# "Sil.aspx?id=" + Eval("KatID") + "&sorgu=DELETE FROM Kategoriler WHERE KatID = @ID&type=kategori" %>'
                                   onclick="return confirm('Bu kategoriyi silmek istediğinizden emin misiniz?')">Sil</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>

        <!-- Kategori Ekle Modal -->
        <div class="modal fade" id="categoryModal" tabindex="-1" role="dialog" aria-labelledby="categoryModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="categoryModalLabel">Kategori Ekle</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <form runat="server">
                            <div class="form-group mb-3">
                                <asp:Label AssociatedControlID="TxtKAd" Text="Kategori Adı" runat="server" CssClass="form-label" />
                                <asp:TextBox ID="TxtKAd" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group mb-3">
                                <asp:Label AssociatedControlID="TxtKAciklama" Text="Kategori Açıklaması" runat="server" CssClass="form-label" />
                                <asp:TextBox ID="TxtKAciklama" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group mb-3">
                                <asp:Label AssociatedControlID="TxtKResimUrl" Text="Kategori Resim URL" runat="server" CssClass="form-label" />
                                <asp:TextBox ID="TxtKResimUrl" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <asp:Button ID="BtnKategoriEkle" Text="Ekle" CssClass="btn btn-primary mb-3" runat="server" OnClick="BtnKategoriEkle_Click" />
                        </form>
                    </div>
                </div>
            </div>
        </div>
    <% 
    } %>
</asp:Content>
