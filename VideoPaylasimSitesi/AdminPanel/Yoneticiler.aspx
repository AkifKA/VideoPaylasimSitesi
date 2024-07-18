<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Admin.Master" AutoEventWireup="true" CodeBehind="Yoneticiler.aspx.cs" Inherits="VideoPaylasimSitesi.AdminPanel.Yoneticiler" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    
    <h2 class="text-center">Yöneticiler</h2>
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
    <div class="d-flex justify-content-end">
        <button type="button" class="btn btn-success mb-4" data-toggle="modal" data-target="#yoneticiEkleModal">Ekle</button>
    </div>
    <table class="table">
        <thead>
            <tr>
                <th scope="col">ID</th>
                <th scope="col">Yönetici Adı</th>
                <th scope="col">Yönetici Soyadı</th>
                <th scope="col">Yönetici Takma Adı</th>
                <th scope="col">Yönetici Email</th>
                <th scope="col">Yönetici Şifresi</th>
                <th scope="col">Yönetici Rolü</th>
                <th scope="col">İşlemler</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>
                        <th scope="row"><%# Eval("YoneticiID") %></th>
                        <td><%# Eval("YoneticiAdi") %></td>
                        <td><%# Eval("YoneticiSoyadi") %></td>
                        <td><%# Eval("YoneticiTakmaAd") %></td>
                        <td><%# Eval("YoneticiEmail") %></td>
                        <td><%# Eval("YoneticiSifre") %></td>
                        <td><%# Eval("YoneticiRol") %></td>                
                        <td>
                            <a href='<%# ResolveUrl("~/AdminPanel/YetkiDuzenle.aspx?YoneticiID=" + Eval("YoneticiID")) %>'>Yetki Düzenle</a> | 
                            <a href='<%# "Sil.aspx?id=" + Eval("YoneticiID") + "&sorgu=DELETE FROM Yoneticiler WHERE YoneticiID = @ID&type=yonetici" %>'
                               onclick="return confirm('Bu yöneticiyi kaldırmak istediğinizden emin misiniz?')">Kaldır</a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>

    <!-- Yönetici Ekle Modal -->
    <div class="modal fade" id="yoneticiEkleModal" tabindex="-1" role="dialog" aria-labelledby="yoneticiEkleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="yoneticiEkleModalLabel">Yönetici Ekle</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <form runat="server">
                        <div class="form-group">
                            <asp:Label AssociatedControlID="TxtYoneticiAdi" Text="Yönetici Adı" runat="server" CssClass="form-label" />
                            <asp:TextBox ID="TxtYoneticiAdi" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label AssociatedControlID="TxtYoneticiSoyadi" Text="Yönetici Soyadı" runat="server" CssClass="form-label" />
                            <asp:TextBox ID="TxtYoneticiSoyadi" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label AssociatedControlID="TxtYoneticiTakmaAd" Text="Yönetici Takma Adı" runat="server" CssClass="form-label" />
                            <asp:TextBox ID="TxtYoneticiTakmaAd" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label AssociatedControlID="TxtYoneticiEmail" Text="Yönetici Email" runat="server" CssClass="form-label" />
                            <asp:TextBox ID="TxtYoneticiEmail" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label AssociatedControlID="TxtYoneticiSifre" Text="Yönetici Şifresi" runat="server" CssClass="form-label" />
                            <asp:TextBox ID="TxtYoneticiSifre" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label AssociatedControlID="DdlYoneticiRol" Text="Yönetici Rolü" runat="server" CssClass="form-label" />
                            <asp:DropDownList ID="DdlYoneticiRol" CssClass="form-control" runat="server">
                                <asp:ListItem Text="--Seçiniz--" Value=""></asp:ListItem>
                                <asp:ListItem Text="Süper Yönetici" Value="Süper Yönetici"></asp:ListItem>
                                <asp:ListItem Text="Video Düzenleyici" Value="Video Düzenleyici"></asp:ListItem>
                                <asp:ListItem Text="Yorum Düzenleyici" Value="Yorum Düzenleyici"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <asp:Label AssociatedControlID="TxtYoneticiResUrl" Text="Yönetici Resim URL" runat="server" CssClass="form-label" />
                           <asp:TextBox ID="TxtYoneticiResUrl" runat="server" ReadOnly="true" Style="display:none;"   />
                        </div>
                        <div class="form-group">
                            <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server" />
                        </div>
                        <div class="form-group">
                            <asp:Button ID="BtnYoneticiEkle" Text="Ekle" CssClass="btn btn-primary mb-3" runat="server" OnClick="BtnYoneticiEkle_Click" />
                            <asp:Button ID="BtnTemizle" Text="Temizle" CssClass="btn btn-secondary mb-3" runat="server" OnClick="BtnTemizle_Click" />
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>

    <% } %>

</asp:Content>
