$(document).ready(function () {
    var width = 960,
        height = 500,
        radius = Math.min(width, height) / 2;

    var color = d3.scale.ordinal()
        .range(["#ffbf00", "#cc5500", "#d2691e", "#ff3800"]);

    var arc = d3.svg.arc()
        .outerRadius(radius - 10)
        .innerRadius(0);

    var pie = d3.layout.pie()
        .sort(null)
        .value(function (category) { return category.Posts; });

    var svg = d3.select("#pieChart").append("svg")
        .attr("width", width)
        .attr("height", height)
      .append("g")
        .attr("transform", "translate(" + width / 2 + "," + height / 2 + ")");

    $.ajax({
        url: '/statistics/PublicityDivisionByCategory',
        type: 'POST',
        contentType: 'application/json',
    }).done(function (categories) {
        categories.forEach(function (category) {
            category.Posts = +category.Posts;
        });

        var g = svg.selectAll(".arc")
            .data(pie(categories))
          .enter().append("g")
            .attr("class", "arc");

        g.append("path")
            .attr("d", arc)
            .style("fill", function (d) {
                return color(d.data.Category);
            })
            .on('click', function (d) { window.location = "/Search/GetPostsByCategory?category=" + d.data.Id; });

        g.append("text")
            .attr("transform", function (d) { return "translate(" + arc.centroid(d) + ")"; })
            .attr("dy", ".35em")
            .style("text-anchor", "middle")
            .text(function (d) { return d.data.Category; });
    });
});