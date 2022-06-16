<%@ Page Language="C#" MasterPageFile="~/ItemBidding.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Item_Bidding_System.General.HomePage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
 
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<link type="text/css" rel="stylesheet" href="SlideShow.css" />


<div class="week-new-text">Today's New</div>

<div class="slideshow-big-container">
<div class="slideshow-container">
    <div class="slide-only-container">
        
         <!-- Full-width images with number and caption text -->
  <div class="mySlides fade">
      <a href="Product.aspx">
          <img src="https://cdn.pixabay.com/photo/2015/04/23/22/00/tree-736885__480.jpg" />
      </a>
  </div>

  <div class="mySlides fade">
      <img src="https://letsenhance.io/static/b8eda2f8914d307d52f725199fb0c5e6/62e7b/MainBefore.jpg" />
  </div>

  <div class="mySlides fade">
      <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSo17BIFmd_BJDrhwy1l_2u9uRAOKHPlghZ21zOeM1_R9-coQV9ROWjImCtJcm-6cThPbo&usqp=CAU" />
  </div>
    </div>

  <!-- Next and previous buttons -->
  <a class="prev" onclick="plusSlides(-1,0)">&#10094;</a>
  <a class="next" onclick="plusSlides(1,0)">&#10095;</a>
</div>
<br>

<!-- The dots/circles -->
<div style="text-align:center">
  <span class="dot1" onclick="currentSlide(1,0)"></span>
  <span class="dot1" onclick="currentSlide(2,0)"></span>
  <span class="dot1" onclick="currentSlide(3,0)"></span>
</div>
</div>

<!--Slideshow 2-->
<div class="week-new-text">Today's Hot</div>

<div class="slideshow-big-container">
<div class="slideshow-container">
    <div class="slide-only-container">
        
         <!-- Full-width images with number and caption text -->
  <div class="mySlides2 fade">
      <img src="https://cdn.pixabay.com/photo/2015/04/23/22/00/tree-736885__480.jpg" />
  </div>

  <div class="mySlides2 fade">
      <img src="https://letsenhance.io/static/b8eda2f8914d307d52f725199fb0c5e6/62e7b/MainBefore.jpg" />
  </div>

  <div class="mySlides2 fade">
      <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSo17BIFmd_BJDrhwy1l_2u9uRAOKHPlghZ21zOeM1_R9-coQV9ROWjImCtJcm-6cThPbo&usqp=CAU" />
  </div>
    </div>

  <!-- Next and previous buttons -->
  <a class="prev" onclick="plusSlides(-1,1)">&#10094;</a>
  <a class="next" onclick="plusSlides(1,1)">&#10095;</a>
</div>
<br>

<!-- The dots/circles -->
<div style="text-align:center">
  <span class="dot2" onclick="currentSlide(1,1)"></span>
  <span class="dot2" onclick="currentSlide(2,1)"></span>
  <span class="dot2" onclick="currentSlide(3,1)"></span>
</div>
</div>

<script>
    let slideIndex = [1,1];
    let slideCon = ["mySlides", "mySlides2"];
    let dotCon = ["dot1", "dot2"];

    showSlides(slideIndex[0], 0);
    showSlides(slideIndex[1], 1);

// Next/previous controls
function plusSlides(n, slide_no) {
    showSlides(slideIndex[slide_no] += n, slide_no);
}

// Thumbnail image controls
function currentSlide(n, slide_no) {
  showSlides(slideIndex[slide_no] = n, slide_no);
}

function showSlides(n, slide_no) {
  let i;
  let slides = document.getElementsByClassName(slideCon[slide_no]);
  let dots = document.getElementsByClassName(dotCon[slide_no]);

  if (n > slides.length) {slideIndex[slide_no] = 1}
  if (n < 1) {slideIndex[slide_no] = slides.length }

  for (i = 0; i < slides.length; i++) {
    slides[i].style.display = "none";
  }

  for (i = 0; i < dots.length; i++) {
    dots[i].className = dots[i].className.replace(" active", "");
  }

  slides[slideIndex[slide_no]-1].style.display = "block";
  dots[slideIndex[slide_no]-1].className += " active";
}
</script>
</asp:Content>
