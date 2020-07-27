/* initialize the calendar
     -----------------------------------------------------------------*/

    $('#calendar').fullCalendar({
    header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,basicWeek,basicDay' 
        },
      defaultDate: '2016-11-23',
      navLinks: true, // can click day/week names to navigate views
      editable: true,
      eventLimit: true, // allow "more" link when too many events
      events: [
        {
          title: 'Conexões - Experiências & Networking',
          start: '2016-11-023'
        },
        {
          title: 'Inglês para o Sucesso',
          start: '2016-11-07',
          end: '2016-11-10'
        },
        {
          id: 999,
          title: '3º Encontro por uma Poética Negra',
          start: '2016-11-25T16:00:00'
        },
        {
          id: 999,
          title: '3º Encontro por uma Poética Negra',
          start: '2016-11-16T16:00:00'
        },
        {
          title: 'Conferencia',
          start: '2016-11-26',
          end: '2016-11-28'
        },
        {
          title: 'Semana de Arquitetura e Design',
          start: '2016-11-28T10:30:00',
          end: '2016-11-30T12:30:00'
        },
        {
          title: 'Almoço da Equipe',
          start: '2016-11-30T12:00:00'
        },
        {
          title: 'Reunião CRM',
          start: '2016-11-29T14:30:00'
        },
        {
          title: 'Lançamento Escola de Liderança',
          start: '2016-11-11T17:30:00'
        },
        {
          title: 'Reunião Portal',
          start: '2016-11-23T20:00:00'
        },
        {
          title: 'Festa GTI',
          start: '2016-11-29T07:00:00'
        },
        {
          title: 'Novo CRM SENAC',
          url: 'http://www.sp.senac.br/jsp/default.jsp?newsID=0',
          start: '2016-11-29'
        }
      ]
    });
    

 
