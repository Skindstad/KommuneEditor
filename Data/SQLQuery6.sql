Alter table Keygruppe add foreign key (Kom_nr) references Kommune;
Alter table Keygruppe add foreign key (GruppeId) references Keysnumber;
Alter table Keygruppe add foreign key (Aarstal) references Aarstal;



Create view GruppeRef As
Select KeyGruppe.Kom_nr, City, Gruppe, Aarstal, tal
From KeyGruppe Join Keysnumber on KeyGruppe.GruppeId = Keysnumber.Id
Join Kommune on KeyGruppe.Kom_nr = Kommune.Kom_nr