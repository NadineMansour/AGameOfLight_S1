class TeachersController < ApplicationController
	def show
	end


  def view_courses

   @sub = Subject.where(school: current_teacher.school)
   @taken_id=TeacherRequestSubject.where(teacher: current_teacher , verified: true ).pluck(:subject_id)
   @subjects=@sub.where.not(id:@taken_id) #i have all records with false not accepted and nill pending
   @not_accepted=TeacherRequestSubject.where(teacher: current_teacher , verified: false ).pluck(:subject_id)
   @subjects=@subjects.where.not(id:@not_accepted) 
   @not_yet=TeacherRequestSubject.where(teacher: current_teacher , verified: nil ).pluck(:subject_id)
   @subjects2=@subjects.where(id:@not_yet) #3shan azhr gambha pending 
   @subjects=@subjects.where.not(id:@not_yet) 

   

  end


  def  request_subject
     
     @request=TeacherRequestSubject.new
     @request.teacher_id=current_teacher.id
     @request.subject_id=params[:subject_id]
     @request.school=current_teacher.school
     #@request.school_admin_id=current_teacher.school_admin_id
     @request.save
     redirect_to view_courses_teachers_path
     
   end 


    def view_game_records
		@current_teacher = current_teacher
		if @current_teacher && @current_teacher.verified
			# get students of their school who are verified to display their scores in the view
			@students = Student.where("school = ? AND verified = ?" , 
				@current_teacher.school, true)
		else
			@students = {}
		end
	end


	def send_message
    @message=Mess.new
    @var=params[:student_id]
  end 


  def submit 
      @message=Mess.new
      @message.semail=current_teacher.email
      @message.remail=Student.find(params[:student_id]).email
      @message.text=params[:my_input]

      @message.save
      redirect_to view_school_verified_students_teachers_path
  end 



	def view_school_verified_students
		@current_teacher = current_teacher
		# get the sorting method if available
		@method = params[:sort_method]
		@order = params[:order_method]
		if @current_teacher && @current_teacher.verified
			@students = Student.where("school = ? AND verified = ?" , 
				@current_teacher.school, true)
			# sort only if there sre students and if the school admin wants to sort
			if @method && @students && @order
				if @method == "1" #sort by grade
					if @order == "1"
						@students = @students.order(grade: :asc)
					else
						@students = @students.order(grade: :desc)
					end
				elsif @method == "2" #sort by name
					if @order == "1"
						@students = @students.order(student_name: :asc)
					else
						@students = @students.order(student_name: :desc)
					end
				elsif @method == "3" #sort by signing up date
					if @order == "1"
						@students = @students.order(created_at: :asc)
					else
						@students = @students.order(created_at: :desc)
					end
				end
			end
		else
			@students = {}
		end
	end
end