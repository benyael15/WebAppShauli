﻿$(document).ready(function () {

    var margin = { top: 40, right: 20, bottom: 30, left: 40 },
        width = 960 - margin.left - margin.right,
        height = 500 - margin.top - margin.bottom;

    var format = d3.format("d");


    var x = d3.scale.ordinal()
        .rangeRoundBands([0, width], .1);

    var y = d3.scale.linear().
        range([height, 0]);

    var xAxis = d3.svg.axis()
        .scale(x)
        .orient("bottom");

    var yAxis = d3.svg.axis()
        .scale(y)
        .orient("left")
        .ticks(2, format);

    var tip = d3.tip()
      .attr('class', 'd3-tip')
      .offset([-10, 0])
      .html(function (d) {
          return "<strong>Comments:</strong> <span style='color:red'>" + d.CommentsCount + "</span>";
      })

    var svg = d3.select(".body").append("svg")
        .attr("width", width + margin.left + margin.right)
        .attr("height", height + margin.top + margin.bottom)
      .append("g")
        .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

    svg.call(tip);

    $.ajax({
        url: '/statistics/GetPopularComments',
        type: 'POST',
        contentType: 'application/json',
    }).done(function (posts) {
        x.domain(posts.map(function (d) { return d.Post; }));
        y.domain([0, d3.max(posts, function (d) { return d.CommentsCount; })]);

        svg.append("g")
            .attr("class", "x axis")
            .attr("transform", "translate(0," + height + ")")
            .call(xAxis);

        svg.append("g")
            .attr("class", "y axis")
            .call(yAxis)
          .append("text")
            .attr("transform", "rotate(-90)")
            .attr("y", 6)
            .attr("dy", ".71em")
            .style("text-anchor", "end")
            .text("Posts");

        svg.selectAll(".bar")
            .data(posts)
          .enter().append("rect")
            .attr("class", "bar")
            .attr("x", function (d) { return x(d.Post); })
            .attr("width", x.rangeBand())
            .attr("y", function (d) { return y(d.CommentsCount); })
            .attr("height", function (d) { return height - y(d.CommentsCount); })
            .on('mouseover', tip.show)
            .on('mouseout', tip.hide)
            .on('click', function (d) { window.location = '/#post ' + d.Id; })
    });
});