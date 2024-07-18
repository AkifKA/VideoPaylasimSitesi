<%@ Page Title="" Language="C#" MasterPageFile="~/UserInterface/User.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="VideoPaylasimSitesi.UserInterface.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="site_head" runat="server">
        
    <link href="Sitiller/Index.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="site_content" runat="server">
    
    <div class="container">
        <!-- Slider -->
        <div id="videoCarousel" class="carousel slide" data-ride="carousel">
            <ol class="carousel-indicators">
                <asp:Repeater ID="IndicatorRepeater" runat="server">
                    <ItemTemplate>
                        <li data-target="#videoCarousel" data-slide-to='<%# Eval("VideoID") %>' class='<%# Convert.ToInt32(Eval("VideoID")) == 1 ? "active" : "" %>'></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ol>
            <div class="carousel-inner">
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <div class='carousel-item <%# Convert.ToInt32(Eval("VideoID")) == 1 ? "active" : "" %>'>
                            <img class="d-block slider-img" src='<%# Eval("VideoUrl") %>' alt="Slide">
                            <div class="carousel-caption d-none d-md-block">
                                <h5><%# Eval("VideoBaslik") %></h5>
                                <p><%# Eval("VideoAciklama") %></p>
                                <a href='VideoDetay.aspx?VideoID=<%# Eval("VideoID") %>' class="btn btn-primary w-25">İzle</a>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <a class="carousel-control-prev" href="#videoCarousel" role="button" data-slide="prev">
                <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                <span class="sr-only">Previous</span>
            </a>
            <a class="carousel-control-next" href="#videoCarousel" role="button" data-slide="next">
                <span class="carousel-control-next-icon" aria-hidden="true"></span>
                <span class="sr-only">Next</span>
            </a>
        </div>
    
        <!-- Video Serileri -->
        <div class="row mt-4 mflex-grow-1">
            <div class="col-12">
                <h2 class="mt-2 mb-2">Video Serileri</h2>
            </div>
            <asp:Repeater ID="Repeater3" runat="server">
                <ItemTemplate>
                    <div class="col-md-3 mb-4">
                        <div class="card h-100">
                            <img class="card-img-top fixed-size" src='<%# Eval("AltKatResUrl") %>' alt='<%# Eval("AltKatResUrl") %>'>
                            <div class="card-body d-flex flex-column justify-content-between flex-grow-1">
                                <h5 class="card-title"> <%# Eval("AltKatAd") %></h5>
                                <p class="card-text">Açıklama: <%# Eval("AltKatAd") %></p>
                                <p class="card-text">Üst Kategorisi: <%# Eval("KatAd") %></p>
                                 <a href='KategorilerineGoreVideolar.aspx?AltKatID=<%# Eval("AltKatID")%>' class="btn btn-outline-success ">İncele</a>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
            <hr/>
          <!-- Kategoriler -->
                <div class="row mt-4">
            <div class="col-12">
                <h2>Kategoriler</h2>
            </div>
            <asp:Repeater ID="Repeater4" runat="server">
                <ItemTemplate>
                    <div class="col-md-3 mb-4">
                        <div class="card h-100">
                            <img class="card-img-top fixed-size" src='<%# Eval("KatResUrl") %>' alt="Category thumbnail">
                            <div class="card-body d-flex flex-column justify-content-between flex-grow-1">
                                <h5 class="card-title"><%# Eval("KatAd") %></h5>
                                <p class="card-text"><%# Eval("KatAciklama") %></p>
                                 <a href='KategorilerineGoreAltKategoriler.aspx?KatID=<%# Eval("KatID")%>' class="btn btn-outline-dark">İncele</a>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <hr />

          <!-- Video Galerisi -->
  <div class="row mt-4">
      <div class="col-12">
          <h2>Tüm Videolar</h2>
      </div>
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
     <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</asp:Content>
