create table if not exists specialty
(
    id            serial  not null primary key,
    code          varchar not null,
    caption       varchar not null,
    qualification varchar not null,
    active        bool    not null default (true),
    constraint specialty_uq unique (qualification)
);

create table if not exists study_group
(
    number    varchar not null primary key,
    course    integer not null default (1),
    specialty integer not null,
    constraint study_group_fk0 foreign key (specialty) references specialty (id),
    constraint study_group_check check ( course > 0 and course < 6 )
);

create table if not exists student
(
    student_id  serial  not null primary key,
    first_name  varchar not null,
    last_name   varchar not null,
    study_group varchar not null,
    constraint student_fk0 foreign key (study_group) references study_group (number)
);

create table if not exists user_role
(
    caption varchar primary key
);

create table if not exists employee
(
    phone      varchar not null primary key,
    pass       varchar not null,
    first_name varchar not null,
    last_name  varchar not null,
    user_role  varchar not null,
    constraint employee_fk foreign key (user_role) references user_role (caption)
);