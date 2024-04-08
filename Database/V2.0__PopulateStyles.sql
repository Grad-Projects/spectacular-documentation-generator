INSERT INTO public.styles (styleName, styleContent)
VALUES ('Simple', '@import url(''https://fonts.googleapis.com/css2?family=Lato:ital,wght@0,100;0,300;0,400;0,700;0,900;1,100;1,300;1,400;1,700;1,900&display=swap'');

body {
    background-color: black;
    color: white;
    font-family: Lato;
}

        
th, td { 
    border: 3px solid #685c6d; 
    text-align: left; 
    padding: 8px; 
    color:rgb(255, 255, 255);
}

th { background-color: #3c3434; }

ul{
    list-style-type: "💡";
    color:rgb(255, 255, 255); 
    font-family: Lato;
}');

INSERT INTO public.styles (styleName, styleContent)
VALUES ('Serious Business', '@import url(''https://fonts.googleapis.com/css2?family=Open+Sans:ital,wght@0,300..800;1,300..800&display=swap'');

body {
    background-image: url("https://images.pexels.com/photos/952670/pexels-photo-952670.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1");
    background-color: #cccccc;
    font-family: Open Sans;
    
}

h1 {
    background-color: rgb(0, 0, 0);
    display: inline-block;
    padding: 0.3%;
    color: white;
    background-size: cover;
    background-position: center;
    background-repeat: no-repeat;

}

h2 {
    background-color: rgb(0, 0, 0);
    padding: 0.2%;
    color: white;
}

p{
    font-weight: bold;
    background-color: rgb(0, 0, 0);
    display: inline-block;
    padding: 0.5%;
    margin: 0.5%;
    color: white;
}

table { border-collapse: collapse; width: 100%; }
        
th, td { 
    border: 3px solid #444574; 
    text-align: left; 
    padding: 8px; 
    color:rgb(255, 255, 255);
}

th { background-color: #37457e; }

ul{
    list-style-type: "⚙️";
    color:rgb(255, 255, 255); 
}'),
('Pastel Dreams','@import url(''https://fonts.googleapis.com/css2?family=Signika:wght@300..700&display=swap'');

body {
    background-image: linear-gradient(rgb(184, 223, 251), rgb(255, 160, 225));
    font-family: Signika;
}

h1 {
    background-color: rgb(176, 150, 221);
    display: inline-block;
    border-radius: 0.5em;
    padding: 0.5%;
    color: white;

}

h2 {
    background-color: rgb(179, 153, 225);
    color: white;
    padding: 0.2%;
    border-radius: 0.5em;
}

p{
    font-weight: bold;
    background-color: rgb(169, 145, 210);
    display: inline-block;
    border-radius: 0.5em;
    padding: 0.5%;
    margin: 1%;
    color: white;
}

table { border-collapse: collapse; width: 100%; }
        
th, td { 
    border: 3px solid #f1dbfb; 
    text-align: left; 
    padding: 8px; 
    color:rgb(83, 54, 99);
}

th { background-color: #f1dbfb; }

ul{
    list-style-type: "💗";
    color:rgb(83, 54, 99); 
}'),
 ('Eye Searer', '@import url(''https://fonts.googleapis.com/css2?family=Bungee+Spice&family=Creepster&family=Lato:ital,wght@0,100;0,300;0,400;0,700;0,900;1,100;1,300;1,400;1,700;1,900&display=swap'');
body {
    font-family: Creepster;
    background-color: rgb(234, 255, 0);
}

h1 {
    display: inline-block;
    border-radius: 0.5em;
    padding: 0.5%;
    color:rgb(255, 255, 255); 

}

h2 {
    color: white;
    padding: 0.2%;
    border-radius: 0.5em;
}

p{
    font-weight: bold;
    display: inline-block;
    border-radius: 0.5em;
    padding: 0.5%;
    margin: 1%;
    color:rgb(255, 255, 255); 
}

table { border-collapse: collapse; width: 100%; }
        
th, td { 
    border: 3px solid #ff0000; 
    text-align: left; 
    padding: 8px; 
    color:rgb(255, 255, 255); 
}

th { background-color: #6200ff; }

ul{
    list-style-type: "👁️";
    color:rgb(255, 255, 255); 
}'),
 ('I Love To Code','@import url(''https://fonts.googleapis.com/css2?family=Bungee+Spice&family=Comic+Neue:ital,wght@0,300;0,400;0,700;1,300;1,400;1,700&family=Creepster&family=Lato:ital,wght@0,100;0,300;0,400;0,700;0,900;1,100;1,300;1,400;1,700;1,900&display=swap'');

body {
    background-image: linear-gradient(rgb(35, 67, 39), rgb(255, 253, 160));
    font-family: Comic Neue;
    font-weight: bolder;
    transform: rotate(180deg); 
}

h1 {
    background-color: rgb(36, 22, 60);
    display: inline-block;
    padding: 0.5%;
    color: red;

}

h2 {
    background-color: rgb(81, 24, 24);
    color: red;
    padding: 0.2%;
}

p{
    font-weight: bold;
    background-color: rgb(20, 48, 88);
    display: inline-block;
    padding: 0.5%;
    margin: 1%;
    color: red;
}

table { border-collapse: collapse; width: 100%; }
        
th, td { 
    border: 3px solid #f1dbfb; 
    text-align: left; 
    padding: 8px; 
    color:rgb(240, 213, 255);
}

th { background-color: #12575f; }

ul{
    list-style-type: "😎";
    color: red;; 
}');
