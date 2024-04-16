create database bdEmprestimo;
use bdEmprestimo;

create table tbUsuario(
codUsu int primary key auto_increment,
nomeUsu varchar(50)
);

create table tbLivro(
codLivro int primary key auto_increment,
nomeLivro varchar(50),
imagemLivro varchar(255)
);

drop table tbLivro;

create table tbEmprestimo(
codEmp int primary key auto_increment,
dataEmp varchar(20),
dataDev varchar(20),
codUsu int references tbUsuario(codUsu)
);

create table itensEmp(
codItem int primary key auto_increment,
codEmp int references tbEmprestimo(codEmp),
codLivro int references tbLivro(codLivro)
)

select * from itensEmp;

insert into tbUsuario values (default,'Nilson'),(default,'Bruno');