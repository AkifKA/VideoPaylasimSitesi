<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.Master" AutoEventWireup="true" CodeBehind="AltKategoriler.aspx.cs" Inherits="VideoPaylasimSitesi.AdminPanel.AltKategoriler" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <h2 class="text-center">Alt Kategoriler</h2>
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
            <button type="button" class="btn btn-success mb-4" data-toggle="modal" data-target="#subCategoryModal">Ekle</button>
        </div>
        <table class="table">
            <thead>
                <tr>
                    <th scope="col">ID</th>
                    <th scope="col">Alt Kategori Adı</th>
                    <th scope="col">Alt Kategori Açıklaması</th>
                    <th scope="col">Üst Kategorisi</th>
                    <th scope="col">Resim URL</th>
                    <th scope="col" class="text-center">İşlemler</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <tr>
                            <th scope="row"><%# Eval("AltKatID") %></th>
                            <td><%# Eval("AltKatAd") %></td>
                            <td><%# Eval("AltKatAciklama") %></td>
                            <td><%# Eval("KatAd") %></td>
                            <td><%# Eval("AltKatResUrl") %></td>
                            <td class="text-center">
                                <a href='<%# ResolveUrl("~/AdminPanel/AltKategoriGuncelle.aspx?AltKatID=" + Eval("AltKatID")) %>'>Düzenle</a> | 
                                <a href='<%# "Sil.aspx?id=" + Eval("AltKatID") + "&sorgu=DELETE FROM AltKategoriler WHERE AltKatID = @ID&type=altkategori" %>'
                                   onclick="return confirm('Bu alt kategoriyi silmek istediğinizden emin misiniz?')">Sil</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>

        <!-- Alt Kategori Ekle Modal -->
        <div class="modal fade" id="subCategoryModal" tabindex="-1" role="dialog" aria-labelledby="subCategoryModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="subCategoryModalLabel">Alt Kategori Ekle</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <form runat="server">
                            <div class="form-group mb-3">
                                <asp:Label AssociatedControlID="TxtAltKAd" Text="Alt Kategori Adı" runat="server" CssClass="form-label" />
                                <asp:TextBox ID="TxtAltKAd" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group mb-3">
                                <asp:Label AssociatedControlID="TxtAltKAciklama" Text="Alt Kategori Açıklaması" runat="server" CssClass="form-label" />
                                <asp:TextBox ID="TxtAltKAciklama" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group mb-3">
                                <asp:Label AssociatedControlID="TxtAltKResimUrl" Text="Alt Kategori Resim URL" runat="server" CssClass="form-label" />
                                <asp:TextBox ID="TxtAltKResimUrl" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>    
                            <div class="form-group mb-3">
                                <asp:Label AssociatedControlID="DrpUstKategori" Text="Üst Kategori" runat="server" CssClass="form-label" />
                                <asp:DropDownList ID="DrpUstKategori" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="" Text="Seçiniz" />
                                </asp:DropDownList>
                            </div>
                            <asp:Button ID="BtnAltKategoriEkle" Text="Ekle" CssClass="btn btn-primary mb-3" runat="server" OnClick="BtnAltKategoriEkle_Click" />
                        </form>
                    </div>
                </div>
            </div>
        </div>
    <% 
    } %>
</asp:Content>
