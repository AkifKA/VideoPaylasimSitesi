<%@ Page Title="" Language="C#" MasterPageFile="~/UserInterface/User.Master" AutoEventWireup="true" CodeBehind="Hakkimizda.aspx.cs" Inherits="VideoPaylasimSitesi.UserInterface.Hakkimizda" %>

<asp:Content ID="Content1" ContentPlaceHolderID="site_head" runat="server">
    <link href="Sitiller/Hakkimizda.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="site_content" runat="server">
    <div class="about-section text-center">
        <div class="container">
            <div class="content">
                <h2>Hakkımızda</h2>
                <p>MAK Video Paylaşım Sitesi, en iyi Arapça Türkçe alt yazılı videoları sunmayı amaçlayan bir platformdur. Ekibimiz, yüksek kaliteli içerik ve mükemmel kullanıcı deneyimi sağlamak için çalışmaktadır. Her türlü geri bildirim ve öneri için bizimle iletişime geçebilirsiniz.</p>
            </div>

            <div class="team">
                <h2>Ekibimiz</h2>
                <div class="row">
                    <div class="col-md-4 team-member">
                        <img src="Resimler/profil.png" alt="Team Member" />
                        <h5>Mehmet Akif Karaöz</h5>
                        <span>Kurucu / Web Geliştirici</span>
                        <div class="social-icons">
                            <a href="https://www.facebook.com/muterjim/"><i class="fab fa-facebook-f"></i></a>
                            <a href="https://x.com/makceviriatlysi"><i class="fab fa-twitter"></i></a>
                            <a href="#"><i class="fab fa-instagram"></i></a>
                            <a href="https://www.linkedin.com/in/akifka/"><i class="fab fa-linkedin"></i></a>
                            <a href="https://www.youtube.com/channel/UCA0AWOu1RIt5TNvVe2y779w"><i class="fab fa-youtube"></i></a>

                        </div>
                    </div>
                    <div class="col-md-4 team-member">
                        <img src="Resimler/profil.png" alt="Team Member" />
                        <h5>Mehmet Akif Karaöz</h5>
                        <span>Video Edit / Alt Yazı Sorumlusu</span>
                        <div class="social-icons">
                            <a href="#"><i class="fab fa-facebook-f"></i></a>
                            <a href="#"><i class="fab fa-twitter"></i></a>
                            <a href="#"><i class="fab fa-instagram"></i></a>
                            <a href="#"><i class="fab fa-linkedin"></i></a>
                            <a href="https://www.youtube.com/channel/UC19i_ouXQeVb-i-sMRSf0sA"><i class="fab fa-youtube"></i></a>
                        </div>
                    </div>
                    <div class="col-md-4 team-member">
                        <img src="Resimler/profil.png" alt="Team Member" />
                        <h5>Mehmet Akif Karaöz</h5>
                        <span>Arapça Türkçe Çeviri Uzmanı</span>
                        <div class="social-icons">
                            <a href="#"><i class="fab fa-facebook-f"></i></a>
                            <a href="#"><i class="fab fa-twitter"></i></a>
                            <a href="#"><i class="fab fa-instagram"></i></a>
                            <a href="#"><i class="fab fa-linkedin"></i></a>
                            <a href="https://www.youtube.com/channel/UCqtMvgKB-kpDtoYkk7gX-yw"><i class="fab fa-youtube"></i></a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
