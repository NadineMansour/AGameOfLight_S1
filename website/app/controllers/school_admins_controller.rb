class SchoolAdminsController < ApplicationController


	def view_verified_students
		@current_admin = SchoolAdmin.find(params[:id])
		@students = Student.where("school = ? AND verified = ?" , 
			@current_admin.school, true)
		@method = params[:sort_method]
		if @method && @students
			if @method == "1"
				@students = @students.order(grade: :asc)
			elsif @method == "2"
				@students = @students.order(grade: :desc)
			elsif @method == "3"
				@students = @students.order(student_name: :asc)
			elsif @method == "4"
				@students = @students.order(student_name: :desc)
			end
		end
	end


end