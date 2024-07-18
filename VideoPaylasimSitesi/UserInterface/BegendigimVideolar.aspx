<%@ Page Title="" Language="C#" MasterPageFile="~/UserInterface/User.Master" AutoEventWireup="true" CodeBehind="BegendigimVideolar.aspx.cs" Inherits="VideoPaylasimSitesi.UserInterface.BegendigimVideolar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="site_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="site_content" runat="server">
    <div class="container mt-4">
        <h2 class="text-center">Beğendiğim Videolar</h2>
        <div class="row mt-2">
            <asp:Repeater ID="Repeater2" runat="server">
                <ItemTemplate>
                    <div class="col-md-4 mb-4">
                        <div class="card h-100">
                            <img class="card-img-top fixed-size" src='<%# Eval("VideoUrl") %>' alt="Video thumbnail">
                            <div class="card-body d-flex flex-column justify-content-between flex-grow-1">
                                <h5 class="card-title"><%# Eval("VideoBaslik") %></h5>
                                <p class="card-text"><%# Eval("VideoAciklama") %></p>
                                <a href='VideoDetay.aspx?VideoID=<%# Eval("VideoID") %>' class="btn btn-outline-danger">İzle</a>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
