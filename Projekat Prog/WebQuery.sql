create database Aplikacija
use Aplikacija


create table Kategorija(
id int primary key identity(1,1),
ime nvarchar(50),
slika nvarchar(50)
)

create table Korisnik(
id int primary key identity(1,1),
email varchar(50),
lozinka varchar(50),
profileimg varchar(50)
)
create table Prodavnica(
id int primary key identity(1,1),
naziv varchar(50),
adresa varchar(50),
mail varchar(50),
slika varchar(50)
)

create table ProdKategorija(
id int primary key identity(1,1),
prodavnica int,
kategorija int,
cena int
)

create table Termin(
id int primary key identity(1,1),
korisnik int,
prodavnica int,
dan date,
vreme varchar(20),
usluga varchar(50)
)

create table vreme(
id int primary key identity(1,1),
vreme varchar(20)
)



go
create proc MojaZakazivanja
@email varchar(50)
as
set lock_timeout 3000;
begin try
select * from Termin join Korisnik on korisnik=Korisnik.id join Prodavnica on prodavnica=Prodavnica.id where @email= Korisnik.email
end try
Begin Catch
	Return @@error
end Catch
go
create proc Kategorije
as
set lock_timeout 3000;
begin try
	select * from Kategorija
end try
BEgin Catch
	Return @@error
End Catch
go
create proc KorisnikInsert
@email varchar(50),
@sifra varchar(20)
as
set lock_timeout 3000;
begin try
if exists (select top 1 email,lozinka from Korisnik where email = @email and lozinka= @sifra)
	return 1
	else
	insert into Korisnik (email,lozinka) 
	Values (@email,@sifra)
		Return 0;
end try
Begin Catch
	return @@error
End Catch
go
create proc KorisnikLogin
@email varchar(50),
@lozinka varchar(20)
as
set lock_timeout 3000;
begin try
if exists (select top 1 email,lozinka from Korisnik where email = @email and lozinka= @lozinka)
	Begin
		Return 1
	End
	Return 0
end try
Begin Catch
	Return @@error
End Catch
go
create proc NadjiKor
@email varchar(50)
as
set lock_timeout 3000;
begin try
select top 1 id from Korisnik where @email=email
end try
Begin Catch
	Return @@error
End catch
go

create proc NadjiProd
@naziv varchar(50)
as
set lock_timeout 3000;
begin try
select top 1 id from Prodavnica where @naziv=naziv
end try
Begin Catch
	Return @@error
End catch

go

create proc PokaziProd
@kategorija varchar(20)
as
set lock_timeout 3000;
begin try
select Kategorija.ime,Prodavnica.naziv,Prodavnica.mail, Prodavnica.slika as slika, Prodavnica.adresa, ProdKategorija.cena as cena from ProdKategorija join Kategorija on Kategorija.id=ProdKategorija.kategorija join Prodavnica on Prodavnica.id=ProdKategorija.prodavnica where Kategorija.ime = @kategorija
end try
Begin Catch
	Return @@error
end Catch

go
create proc Search
@term varchar(50)
as
set lock_timeout 3000;
begin try
select * from Prodavnica where naziv like @term
end try 
Begin Catch
	Return @@error
end Catch

go
create proc SviTermini
@prodavnica varchar(50)
as
set lock_timeout 3000;
begin try
select * from Termin join Prodavnica on prodavnica=Prodavnica.id where Prodavnica.naziv=@prodavnica
end try
Begin Catch
	Return @@error
end Catch
go
create PROC SviVremena
as
set lock_timeout 3000;
begin try
select * from vreme
end try
Begin Catch
	Return @@error
end Catch
go
create proc UpisiTermin
@korisnik int,
@prodavnica int,
@dan date,
@vreme varchar(10),
@usluga varchar(20)
as
set lock_timeout 3000;
begin try 
if exists (select top 1 * from Termin where korisnik=@korisnik and @prodavnica=prodavnica and @dan=dan and vreme=@vreme)
		Begin
			Return 0
		End
	Insert Into Termin (korisnik,prodavnica,dan,vreme,usluga) Values (@korisnik,@prodavnica,@dan,@vreme,@usluga)
	Return 1
end try
Begin Catch
	Return @@error
End catch
go
create proc OstalaVremena
@prodavnica varchar(50),
@date date
as
set lock_timeout 3000;
begin try
select *from vreme where vreme not in (select Termin.vreme from Termin join Prodavnica on prodavnica=Prodavnica.id where Prodavnica.naziv=@prodavnica and Termin.dan =@date)
end try
Begin Catch
	Return @@error
end Catch
execute OstalaVremena 'Zlato', '2021-05-05' 
