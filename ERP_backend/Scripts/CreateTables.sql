USE erp

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name like 'kategorija') create table kategorija(
	IDKategorija int primary key identity(1,1),
	naziv nvarchar(50) not null,
	opis nvarchar(300) not null
);
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name like 'kolekcija') create table kolekcija(
	IDKolekcija int primary key identity(1,1),
	naziv nvarchar(50) not null,
	opis nvarchar(300) not null
);
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name like 'skladiste') create table skladiste(
	IDSkladiste int primary key identity(1,1),
	naziv nvarchar(50) not null,
	adresa nvarchar(200) not null
);
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name like 'proizvodjac') create table proizvodjac(
	IDProizvodjac int primary key identity(1,1),
	naziv nvarchar(50) not null,
	adresa nvarchar(200) not null
);
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name like 'korisnik') create table korisnik(
	IDKorisnik int primary key identity(1,1),
	tipKorisnika nvarchar(20) not null,
	username nvarchar(25) not null,
	sifra nvarchar(25) not null,
	ime nvarchar(15) not null,
	prezime nvarchar(20) not null,
	mail nvarchar(50) not null,
	grad nvarchar(25) not null,
	adresa nvarchar(50) not null
);
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name like 'velicina') create table velicina(
	IDVelicina int primary key identity(1,1),
	oznaka nvarchar(10) not null
);
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name like 'proizvodInfo') create table proizvodInfo(
	IDInfo int primary key identity(1,1),
	naziv nvarchar(50) not null,
	opis nvarchar(300) not null,
	cena numeric(8,2) not null,
);
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name like 'proizvod') create table proizvod(
	IDProizvod int primary key identity(1,1),
	IDInfo int not null,
	IDVelicina int not null,
	IDProizvodjac int not null,
	IDKategorija int not null,
	IDKolekcija int not null,
	stanje bit not null,
	ukupnaKolicina int not null,

	constraint FK_Proizvod_ProizvodInfo foreign key (IDInfo) references proizvodInfo(IDInfo),
	constraint FK_Proizvod_Proizvodjac foreign key (IDProizvodjac) references proizvodjac(IDProizvodjac),
	constraint FK_Proizvod_Velicina foreign key (IDVelicina) references velicina(IDVelicina),
	constraint FK_Proizvod_Kategorija foreign key (IDKategorija) references kategorija(IDKategorija),
	constraint FK_Proizvod_Kolekcija foreign key (IDKolekcija) references kolekcija(IDKolekcija)
);
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name like 'slika') create table slika(
	IDSlika int primary key identity(1,1),
	adresa nvarchar(100) not null,
	tipSlike nvarchar(10) not null,
	nazivSlike nvarchar(50) not null
);
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name like 'proizvodSlika') create table proizvodSlika(
	IDProizvod int not null,
	IDSlika int not null,

	constraint PK_ProizvodSlika primary key (IDProizvod, IDSlika),
	constraint FK_ProizvodSlika_Proizvod foreign key (IDProizvod) references proizvod(IDProizvod),
	constraint FK_ProizvodSlika_Slika foreign key (IDSlika) references slika(IDSlika)
);
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name like 'proizvodSkladiste') create table proizvodSkladiste(
	IDProizvod int not null,
	IDSkladiste int not null,
	kolicina int not null,

	constraint PK_Stanje primary key (IDProizvod, IDSkladiste),
	constraint FK_Stanje_Proizvod foreign key (IDProizvod) references proizvod(IDProizvod),
	constraint FK_Stanje_Skladiste foreign key (IDSkladiste) references skladiste(IDSkladiste)
);
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name like 'stavkaKorpe') create table stavkaKorpe(
	IDKupac int not null,
	IDProizvod int not null,
	kolicina int not null,

	constraint PK_StavkaKorpe primary key (IDProizvod, IDKupac),
	constraint FK_StavkaKorpe_Proizvod foreign key (IDProizvod) references proizvod(IDProizvod),
	constraint FK_StavkaKorpe_Korisnik foreign key (IDKupac) references korisnik(IDKorisnik)
);
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name like 'racun') create table racun(
	IDRacun int primary key identity(1,1),
	IDKupac int not null,
	ukupnaCena numeric(9,2) not null,
	datum date not null,

	constraint FK_Racun_Korisnik foreign key (IDKupac) references korisnik(IDKorisnik)
);
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name like 'stavkaRacuna') create table stavkaRacuna(
	IDRacun int not null,
	IDStavkaRacuna int identity(1,1),
	IDProizvod int not null,
	kolicina int not null,
	cena numeric (8,2) not null,

	constraint PK_StavkaRacuna primary key (IDRacun, IDStavkaRacuna),
	constraint FK_StavkaRacuna_Racun foreign key (IDRacun) references racun(IDRacun),
	constraint FK_StavkaRacuna_Proizvod foreign key (IDProizvod) references proizvod(IDProizvod)
);